//-----------------------------------------------------------------------
// <copyright file="AccessSdk.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Service
{
    using KountAccessSdk.Interfaces;
    using KountAccessSdk.Log.Factory;
    using KountAccessSdk.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Security.Authentication;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class AccessSdk
    {
        /// <summary>
        /// The Logger to use.
        /// </summary>
        private readonly ILogger _logger;

        private const string DEFAULT_VERSION = "0400";

        private readonly string _apiKey;
        private readonly string _encodedCredentials;
        private readonly string _host;
        private readonly int _merchantId;

        private readonly string _version;
        private readonly IWebClientFactory _webClientFactory;

        /// <summary>
        /// Creates instance of the AccessSdk, allowing the client to specify version of responses to request.
        /// </summary>
        /// <param name="host">host FQDN of the host that AccessSdk will talk to.</param>
        /// <param name="merchantId">merchantId Merchant ID (6 digit value).</param>
        /// <param name="apiKey">apiKey The API Key for the merchant.</param>
        /// <param name="version">version The version of the API response to return.</param>
        /// <param name="webClientFactory">Used for webClient mockup in tests.</param>
        public AccessSdk(string host, int merchantId, string apiKey, string version = DEFAULT_VERSION, IWebClientFactory webClientFactory = null)
        {
            if (webClientFactory == null)
            {
                this._webClientFactory = new SystemWebClientFactory();
            }
            else
            {
                this._webClientFactory = webClientFactory;
            }

            if (apiKey == null)
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Missing apiKey");
            }

            if (String.IsNullOrEmpty(apiKey.Trim()))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid apiKey(" + apiKey + ")");
            }

            if (merchantId < 99999 || merchantId > 1000000)
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid merchantId");
            }

            this._host = host ?? throw new AccessException(AccessErrorType.INVALID_DATA, "Missing host");

            ILoggerFactory factory = LogFactory.GetLoggerFactory();
            this._logger = factory.GetLogger(typeof(AccessSdk).ToString());


            this._merchantId = merchantId;
            this._apiKey = apiKey.Trim();
            this._version = version;
            this._encodedCredentials = GetBase64Credentials(merchantId.ToString(), apiKey);

            var velocityEndpoint = host + "/api/velocity";
            var deviceEndpoint = host + "/api/device";
            var decisionEndpoint = host + "/api/decision";


            this._logger.Info("Access SDK using merchantId = " + merchantId + ", host = " + host);
            this._logger.Debug("velocity endpoint: " + velocityEndpoint);
            this._logger.Debug("decision endpoint: " + decisionEndpoint);
            this._logger.Debug("device endpoint: " + deviceEndpoint);
        }

        /// <summary>
        /// Gets the Decision data for the session's username and password.
        /// </summary>
        /// <param name="sessionId">The Session ID returned from the JavaScript data collector.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>Decision data</returns>
        public DecisionInfo GetDecision(string sessionId, string username, string password)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid sessionid (" + sessionId + ").  Must be 32 characters");
            }

            using (IWebClient client = this._webClientFactory.Create())
            {

                PrepareWebClient((WebClient)client);

                NameValueCollection reqparm = GetRequestedParams(sessionId, username, password);

                try
                {
                    byte[] responsebytes = client.UploadValues("/api/decision", "POST", reqparm);
                    string responsebody = Encoding.UTF8.GetString(responsebytes);
                    DecisionInfo dInfo = JsonConvert.DeserializeObject<DecisionInfo>(responsebody);

                    return dInfo;
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the Decision data async for the session's username and password.
        /// </summary>
        /// <param name="sessionId">The Session ID returned from the JavaScript data collector.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>Decision data</returns>
        public async Task<DecisionInfo> GetDecisionAsync(string sessionId, string username, string password)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid sessionid (" + sessionId + ").  Must be 32 characters");
            }

            using (WebClient client = new WebClient())
            {
                PrepareWebClient(client);

                NameValueCollection reqparm = GetRequestedParams(sessionId, username, password);

                try
                {
                    byte[] responsebytes = await client.UploadValuesTaskAsync("/api/decision", "POST", reqparm);
                    string responsebody = Encoding.UTF8.GetString(responsebytes);
                    return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<DecisionInfo>(responsebody));
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                return null;
            }
        }



        /// <summary>
        /// Gets the device data for the session.
        /// </summary>
        /// <param name="sessionId">The Session ID returned from the JavaScript data collector.</param>
        /// <returns>Device data</returns>
        public DeviceInfo GetDevice(string sessionId)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid sessionid (" + sessionId + ").  Must be 32 characters");
            }

            using (IWebClient client = this._webClientFactory.Create())
            {
                PrepareWebClient((WebClient)client);

                try
                {
                    var endPoint = $"/api/device?s={sessionId}&v={this._version}";
                    string res = client.DownloadString(endPoint);
                    DeviceInfo dInfo = JsonConvert.DeserializeObject<DeviceInfo>(res);

                    return dInfo;
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                return null;
            }
        }


        /// <summary>
        /// Gets the device data async for the session.
        /// </summary>
        /// <param name="sessionId">The Session ID returned from the JavaScript data collector.</param>
        /// <returns>Device data</returns>
        public async Task<DeviceInfo> GetDeviceAsync(string sessionId)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid sessionid (" + sessionId + ").  Must be 32 characters");
            }

            using (WebClient client = new WebClient())
            {
                PrepareWebClient(client);

                try
                {
                    var endPoint = $"/api/device?s={sessionId}&v={this._version}";
                    var res = await client.DownloadStringTaskAsync(endPoint);
                    return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<DeviceInfo>(res));
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the velocity data for the session's username and password.
        /// </summary>
        /// <param name="sessionId">The Session ID returned from the JavaScript data collector.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>Velocity data</returns>
        public VelocityInfo GetVelocity(string sessionId, string username, string password)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid sessionid (" + sessionId + ").  Must be 32 characters");
            }

            using (IWebClient client = this._webClientFactory.Create())
            {
                PrepareWebClient((WebClient)client, true);

                NameValueCollection reqparm = GetRequestedParams(sessionId, username, password);

                try
                {
                    byte[] responsebytes = client.UploadValues("/api/velocity", "POST", reqparm);
                    string responsebody = Encoding.UTF8.GetString(responsebytes);
                    VelocityInfo dInfo = JsonConvert.DeserializeObject<VelocityInfo>(responsebody);

                    return dInfo;
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                return null;
            }
        }


        /// <summary>
        /// Gets the velocity data async for the session's username and password.
        /// </summary>
        /// <param name="sessionId">The Session ID returned from the JavaScript data collector.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>Velocity data</returns>
        public async Task<VelocityInfo> GetVelocityAsync(string sessionId, string username, string password)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid sessionid (" + sessionId + ").  Must be 32 characters");
            }

            using (WebClient client = new WebClient())
            {

                PrepareWebClient(client, true);

                NameValueCollection reqparm = GetRequestedParams(sessionId, username, password);

                try
                {
                    byte[] responsebytes = await client.UploadValuesTaskAsync("/api/velocity", "POST", reqparm);
                    string responsebody = Encoding.UTF8.GetString(responsebytes);
                    return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<VelocityInfo>(responsebody));
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                return null;
            }
        }

        /// <summary>
        /// Get set of data elements for the session's username, password and uniq.
        /// The set of data elements depends of "i" param.
        /// </summary>
        /// <param name="sessionId">The Session ID returned from the JavaScript data collector.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">Customer identifier.</param>
        /// <param name="uniq">The password of the user.</param>
        /// <param name="dse">The requested set of data elements expected in response.</param>
        /// <returns>Set of data elements</returns>
        public Info GetInfo(string sessionId, string username, string password, string uniq, DataSetElements dse)
        {
            int i = dse.Build();
            ValidateGetInfo(sessionId, username, password, uniq, i);

            using (IWebClient client = this._webClientFactory.Create())
            {
                PrepareWebClient((WebClient)client, true);

                NameValueCollection reqparm = GetRequestedParams(sessionId, username, password, uniq, i);

                try
                {
                    byte[] responsebytes = client.UploadValues("/api/info", "POST", reqparm);
                    string responsebody = Encoding.UTF8.GetString(responsebytes);
                    Info info = JsonConvert.DeserializeObject<Info>(responsebody);

                    return info;
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                return null;
            }
        }

        /// <summary>
        /// Get set of data elements async for the session's username, password and uniq.
        /// The set of data elements depends of "i" param.
        /// </summary>
        /// <param name="sessionId">The Session ID returned from the JavaScript data collector.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">Customer identifier.</param>
        /// <param name="uniq">The password of the user.</param>
        /// <param name="dse">The requested set of data elements expected in response.</param>
        /// <returns>Set of data elements</returns>
        public async Task<Info> GetInfoAsync(string sessionId, string username, string password, string uniq, DataSetElements dse)
        {
            int i = dse.Build();
            ValidateGetInfo(sessionId, username, password, uniq, i);

            using (WebClient client = new WebClient())
            {
                PrepareWebClient((WebClient)client, true);

                NameValueCollection reqparm = GetRequestedParams(sessionId, username, password, uniq, i);

                try
                {
                    byte[] responsebytes = await client.UploadValuesTaskAsync("/api/info", "POST", reqparm);
                    string responsebody = Encoding.UTF8.GetString(responsebytes);
                    Info info = JsonConvert.DeserializeObject<Info>(responsebody);

                    return info;
                }
                catch (WebException ex)
                {
                    HandleWebException(ex);
                }
                return null;
            }
        }

        private void PrepareWebClient(WebClient client, bool isPostWithUrlParams = false)
        {
            client.Headers.Add(HttpRequestHeader.Accept, "application/json");
            client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + this._encodedCredentials);

            // WebClient throws WebException when UploadValues method is called and ContentType is different of "application/x-www-form-urlencoded".
            // More info for WebClient UploadValues(string address, string method, NameValueCollection data) method here: https://msdn.microsoft.com/en-us/library/900ted1f%28v=vs.110%29.aspx?f=255&MSPPError=-2147217396
            if (isPostWithUrlParams)
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            }
            else
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            }

            client.BaseAddress = this._host;
            client.Encoding = System.Text.Encoding.UTF8;

        }

        private NameValueCollection GetRequestedParams(string sessionId, string username, string password, string uniq = null, int i = 0, string timing = null, string ts = null)
        {
            NameValueCollection reqparm = new NameValueCollection();

            reqparm.Add("s", sessionId);
            reqparm.Add("v", this._version);

            if (!String.IsNullOrEmpty(username))
            {
                string uh = HashValue(username);
                reqparm.Add("uh", uh);
            }

            if (!String.IsNullOrEmpty(password))
            {
                string ph = HashValue(password);
                reqparm.Add("ph", ph);
            }

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                string ah = HashValue($"{username}:{password}");
                reqparm.Add("ah", ah);
            }

            if (!String.IsNullOrEmpty(uniq))
            {
                reqparm.Add("uniq", uniq);
            }

            if (i > 0 && i <= 31)
            {
                reqparm.Add("i", i.ToString());
            }

            if (!String.IsNullOrEmpty(timing))
            {
                reqparm.Add("timing", timing);
            }

            if (!String.IsNullOrEmpty(ts))
            {
                reqparm.Add("ts", ts);
            }

            return reqparm;
        }

        /// <summary>
        /// Converts the auth header.
        /// </summary>
        /// <param name="merchantId">merchantId Merchant ID (6 digit value).</param>
        /// <param name="apiKey">apiKey The API Key for the merchant.</param>
        /// <returns>encoded Base64Credentials</returns>
        private static string GetBase64Credentials(string merchantId, string apiKey)
        {
            string str2 = String.Format("{0}:{1}", merchantId.Trim(), apiKey.Trim());
            try
            {
                str2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(str2));
            }
            catch (Exception exception)
            {
                throw new AccessException(AccessErrorType.ENCRYPTION_ERROR, "Error in base64Encode: " + exception.Message);
            }
            return str2;
        }

        private static void HandleWebException(WebException webException)
        {
            if (webException.Response == null)
            {
                var msg = webException.Message;
                if (webException.InnerException is AuthenticationException)
                {
                    msg = "Unable to connect. The credentials are not valid.";
                }
                else
                {
                    msg = "Unable to contact server.";
                }

                throw new AccessException(AccessErrorType.NETWORK_ERROR, $"BAD RESPONSE({webException.Status}): {msg}");
            }
            else
            {
                string error;
                string errorDescription;

                using (HttpWebResponse resp = webException.Response as HttpWebResponse)
                {
                    error = $"BAD RESPONSE({resp.StatusCode}):{resp.StatusDescription}";
                    errorDescription = String.Empty;
                    switch (resp.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized://401
                            errorDescription = "UNAUTHORIZED. The credentials are not valid.";
                            break;

                        case HttpStatusCode.InternalServerError://500
                            errorDescription = "UNKNOWN NETWORK ISSUE, try again later.";
                            break;

                        case HttpStatusCode.NotFound://404
                            errorDescription = $"UNKNOWN HOST({resp.ResponseUri.Host}).";
                            break;

                        case HttpStatusCode.ServiceUnavailable://503
                            errorDescription = "The service was not available.";
                            break;

                        case HttpStatusCode.GatewayTimeout://504 TODO
                            errorDescription = "TIMEOUT request.";
                            break;

                        default:
                            errorDescription = "UNKNOWN NETWORK ISSUE.";
                            break;
                    }
                }
                throw new AccessException(AccessErrorType.NETWORK_ERROR, $"{error}. {errorDescription}");
            }
        }

        private static void ValidateGetInfo(string sessionId, string username, string password, string uniq, int i)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid session id (" + sessionId + ").  Must be 32 characters");
            }

            var trusted = new DataSetElements().WithTrusted().Build();
            var behavioSec = new DataSetElements().WithBehavioSec().Build();
            // uniq is required if trusted or behavio sec data is requested
            if ((((i & trusted) == trusted || (i & behavioSec) == behavioSec)) && 
                string.IsNullOrEmpty(uniq))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Parameter \"uniq\" is required.");
            }

            var velocity = new DataSetElements().WithVelocity().Build();
            var decision = new DataSetElements().WithDecision().Build();
            // username and password are required if velocity or decision data is requested
            if ((((i & velocity) == velocity || (i & decision) == decision)) &&
                (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Parameters \"username\" and \"password\" are both required.");
            }
        }

        /// <summary>
        /// Returns a SHA-256 hashed value for a string.
        /// </summary>
        /// <param name="strData">The String to convert</param>
        /// <returns>The converted string.</returns>
        public static string HashValue(string strData)
        {
            var message = Encoding.UTF8.GetBytes(strData);
            string hex = "";

            using (SHA256Managed hashString = new SHA256Managed())
            {
                var hashValue = hashString.ComputeHash(message);
                foreach (byte x in hashValue)
                {
                    hex += String.Format("{0:x2}", x);
                }
            }
            return hex;
        }
    }
}