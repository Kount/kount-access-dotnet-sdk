//-----------------------------------------------------------------------
// <copyright file="IWebClient.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Interfaces
{
    using System;
    using System.Collections.Specialized;

    /// <summary>
    /// Help interface to mock WebClient
    /// </summary>
    public interface IWebClient : IDisposable
    {
        // Required methods (subset of `System.Net.WebClient` methods).
        string DownloadString(string address);

        byte[] UploadValues(string address, string method, NameValueCollection data);
    }
}