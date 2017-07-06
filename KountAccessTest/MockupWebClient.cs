using KountAccessSdk.Interfaces;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace KountAccessTest
{
    /// <summary>
    /// System web client.
    /// </summary>
    public class MockupWebClient : WebClient, IWebClient
    {
        private string jsonResponce = String.Empty;

        public MockupWebClient(string jsonResponce)
        {
            this.jsonResponce = jsonResponce;
        }

        public new string DownloadString(string address)
        {
            if (base.BaseAddress.Equals("gty://bad.host.com/"))
            {
                throw new WebException("Server not responce", WebExceptionStatus.ConnectionClosed);
            }
            return this.jsonResponce;
        }

        public new byte[] UploadValues(string address, string method, NameValueCollection data)
        {
            if (base.BaseAddress.Equals("gty://bad.host.com/"))
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://google.com/");

                WebResponse response = myReq.GetResponse();

                throw new WebException("Protocol Test Error", new Exception("Test exception"), WebExceptionStatus.ProtocolError, response);
            }
            return Encoding.UTF8.GetBytes(this.jsonResponce);
        }
    }

    /// <summary>
    /// System web client factory.
    /// </summary>
    public class MockupWebClientFactory : IWebClientFactory
    {
        public MockupWebClientFactory(string jsonResponce)
        {
            this.jsonResponce = jsonResponce;
        }

        private string jsonResponce = String.Empty;

        public IWebClient Create()
        {
            return new MockupWebClient(this.jsonResponce);
        }
    }

}