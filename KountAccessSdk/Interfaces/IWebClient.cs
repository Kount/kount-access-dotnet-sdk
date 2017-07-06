namespace KountAccessSdk.Interfaces
{
    using System;
    using System.Collections.Specialized;

    public interface IWebClient : IDisposable
    {
        // Required methods (subset of `System.Net.WebClient` methods).
        string DownloadString(string address);

        byte[] UploadValues(string address, string method, NameValueCollection data);
    }
}