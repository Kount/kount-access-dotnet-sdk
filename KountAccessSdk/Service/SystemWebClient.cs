//-----------------------------------------------------------------------
// <copyright file="SystemWebClient.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Service
{
    using KountAccessSdk.Interfaces;
    using System.Net;

    /// <summary>
    /// System web client.
    /// </summary>
    public class SystemWebClient : WebClient, IWebClient
    {
    }

    /// <summary>
    /// System web client factory.
    /// </summary>
    public class SystemWebClientFactory : IWebClientFactory
    {
        public IWebClient Create()
        {
            return new SystemWebClient();
        }
    }
}