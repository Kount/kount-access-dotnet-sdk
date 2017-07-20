//-----------------------------------------------------------------------
// <copyright file="MockupWebClient.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace KountAccessTest
{
    using KountAccessSdk.Interfaces;
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Mockup system web client.
    /// </summary>
    public class MockupWebClient : WebClient, IWebClient
    {
        private string jsonResponse = String.Empty;

        public MockupWebClient(string jsonResponse)
        {
            this.jsonResponse = jsonResponse;
        }

        public new string DownloadString(string address)
        {
            if (base.BaseAddress.Equals("gty://bad.host.com/"))
            {
                throw new WebException("Server not responding.", WebExceptionStatus.ConnectionClosed);
            }
            return this.jsonResponse;
        }

        public new byte[] UploadValues(string address, string method, NameValueCollection data)
        {
            if (base.BaseAddress.Equals("gty://bad.host.com/"))
            {
                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("http://google.com/");

                WebResponse response = myReq.GetResponse();

                throw new WebException("Protocol Test Error", new Exception("Test exception"), WebExceptionStatus.ProtocolError, response);
            }
            return Encoding.UTF8.GetBytes(this.jsonResponse);
        }
    }

    /// <summary>
    /// Mockup web client factory.
    /// </summary>
    public class MockupWebClientFactory : IWebClientFactory
    {
        public MockupWebClientFactory(string jsonResponse)
        {
            this.jsonResponse = jsonResponse;
        }

        private string jsonResponse = String.Empty;

        public IWebClient Create()
        {
            return new MockupWebClient(this.jsonResponse);
        }
    }

}