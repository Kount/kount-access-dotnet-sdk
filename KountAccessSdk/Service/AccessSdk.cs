//-----------------------------------------------------------------------
// <copyright file="AccessSdk.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Service
{
    using KountAccessSdk.Interfaces;
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
        private const string DEAFULT_VERSION = "0210";

        private readonly string _apiKey;
        private readonly string _encodedCredentials;
        private readonly string _host;
        private readonly int _merchantId;

        private readonly string _version;
        private IWebClientFactory webClientFactory;

        /// <summary>
        /// Creates instance of the AccessSdk, allowing the client to specify version of responses to request.
        /// </summary>
        /// <param name="host">host FQDN of the host that AccessSdk will talk to.</param>
        /// <param name="merchantId">merchantId Merchant ID (6 digit value).</param>
        /// <param name="apiKey">apiKey The API Key for the merchant.</param>
        /// <param name="version">version The version of the API response to return.</param>
        /// <param name="webClientFactory">used for webClient mockup in tests.</param>
        public AccessSdk(string host, int merchantId, string apiKey, string version = DEAFULT_VERSION, IWebClientFactory webClientFactory = null)
        {
            if (webClientFactory == null)
            {
                this.webClientFactory = new SystemWebClientFactory();
            }
            else
            {
                this.webClientFactory = webClientFactory;
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

            this._merchantId = merchantId;
            this._apiKey = apiKey.Trim();
            this._version = version;
            this._encodedCredentials = GetBase64Credentials(merchantId.ToString(), apiKey);
        }

        /// <summary>
        /// Gets the Decision data for the session's username and password.
        /// </summary>
        /// <param name="sessionId">The Session ID returned from the Javascript data collector.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>Decision data</returns>
        public DecisionInfo GetDecision(string sessionId, string username, string password)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid sessionid (" + sessionId + ").  Must be 32 characters");
            }

            using (IWebClient client = this.webClientFactory.Create())
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
        /// <param name="sessionId">The Session ID returned from the Javascript data collector.</param>
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
        /// <param name="sessionId">The Session ID returned from the Javascript data collector.</param>
        /// <returns>Device data</returns>
        public DeviceInfo GetDevice(string sessionId)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid sessionid (" + sessionId + ").  Must be 32 characters");
            }

            using (IWebClient client = this.webClientFactory.Create())
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
        /// <param name="sessionId">The Session ID returned from the Javascript data collector.</param>
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
        /// <param name="sessionId">The Session ID returned from the Javascript data collector.</param>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>Velocity data</returns>
        public VelocityInfo GetVelocity(string sessionId, string username, string password)
        {
            if (String.IsNullOrEmpty(sessionId) || (sessionId.Length > 32))
            {
                throw new AccessException(AccessErrorType.INVALID_DATA, "Invalid sessionid (" + sessionId + ").  Must be 32 characters");
            }

            using (IWebClient client = this.webClientFactory.Create())
            {
                PrepareWebClient((WebClient)client);

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
        /// <param name="sessionId">The Session ID returned from the Javascript data collector.</param>
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

                PrepareWebClient(client);

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

        private void PrepareWebClient(WebClient client)
        {
            client.Headers["Authorization"] = "Basic " + this._encodedCredentials;
            client.Headers["Content-Type"] = "text/json";
            client.Headers["Accept"] = "text/json";

            client.BaseAddress = this._host;
            client.Encoding = System.Text.Encoding.UTF8;

        }

        private NameValueCollection GetRequestedParams(string sessionId, string username, string password)
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
                string error = String.Empty;
                string error_description = String.Empty;

                using (HttpWebResponse resp = webException.Response as HttpWebResponse)
                {
                    error = $"BAD RESPONSE({resp.StatusCode}):{resp.StatusDescription}";
                    error_description = String.Empty;
                    switch (resp.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized://401
                            error_description = "UNAUTHORIZED. The credentials are not valid.";
                            break;

                        case HttpStatusCode.InternalServerError://500
                            error_description = "UNKNOWN NETWORK ISSUE, try again later.";
                            break;

                        case HttpStatusCode.NotFound://404
                            error_description = $"UNKNOWN HOST({resp.ResponseUri.Host}).";
                            break;

                        case HttpStatusCode.ServiceUnavailable://503
                            error_description = "The service was not available.";
                            break;

                        case HttpStatusCode.GatewayTimeout://504 TODO
                            error_description = "TIMEOUT request.";
                            break;

                        default:
                            error_description = "UNKNOWN NETWORK ISSUE.";
                            break;
                    }
                }
                throw new AccessException(AccessErrorType.NETWORK_ERROR, $"{error}. {error_description}");
            }
        }

        /// <summary>
        /// Returns a SHA-256 hashed value for a string.
        /// </summary>
        /// <param name="strData">The String to convert</param>
        /// <returns>The converted string.</returns>
        private static string HashValue(string strData)
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