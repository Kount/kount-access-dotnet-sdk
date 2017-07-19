//-----------------------------------------------------------------------
// <copyright file="SimpleLoggerFactory.cs" company="Kount Inc">
//     Copyright Kount. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Log.Factory
{
    using KountAccessSdk.Interfaces;

    /// <summary>
    /// A simple logger binding class.<br/>
    /// <b>Author:</b> Kount <a>custserv@kount.com</a>;<br/>
    /// <b>Copyright:</b> 2017 Kount Inc <br/>
    /// </summary>
    public class SimpleLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Get a simple logger binding.
        /// </summary>
        /// <param name="name">Name of the logger</param>
        /// <returns>A Kount.Log.Binding.SimpleLogger</returns>
        public ILogger GetLogger(string name)
        {
            return new KountAccessSdk.Log.Binding.SimpleLogger(name);
        }
    }
}