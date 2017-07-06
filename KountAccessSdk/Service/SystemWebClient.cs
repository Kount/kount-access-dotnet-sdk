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