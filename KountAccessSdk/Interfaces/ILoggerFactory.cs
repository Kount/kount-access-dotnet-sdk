//-----------------------------------------------------------------------
// <copyright file="ILoggerFactory.cs" company="Kount Inc">
//     Copyright Kount. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Interfaces
{
    /// <summary>
    /// Interface for a logger factory.<br/>
    /// <b>Author:</b> Kount <a>custserv@kount.com</a>;<br/>
    /// <b>Copyright:</b> 2017 Kount Inc <br/>
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Get a logger binding.
        /// </summary>
        /// <param name="name">Name of the logger</param>
        /// <returns>A Kount.Log.Binding.Logger</returns>
        ILogger GetLogger(string name);
    }
}