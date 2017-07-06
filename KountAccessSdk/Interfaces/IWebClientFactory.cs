//-----------------------------------------------------------------------
// <copyright file="IWebClientFactory.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Interfaces
{
    /// <summary>
    /// Help interface to mock WebClient
    /// </summary>
    public interface IWebClientFactory
    {
        IWebClient Create();
    }
}