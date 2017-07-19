//-----------------------------------------------------------------------
// <copyright file="NopLoggerFactory.cs" company="Kount Inc">
//     Copyright Kount. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Log.Factory
{
    using KountAccessSdk.Interfaces;
    using KountAccessSdk.Log.Binding;

    /// <summary>
    /// A NOP logger binding class.<br/>
    /// <b>Author:</b> Kount <a>custserv@kount.com</a>;<br/>
    /// <b>Copyright:</b> 2017 Kount Inc <br/>
    /// </summary>
    public class NopLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Get a NOP logger binding.
        /// </summary>
        /// <param name="name">Name of the logger</param>
        /// <returns>A Kount.Log.Binding.NopLogger</returns>
        public ILogger GetLogger(string name)
        {
            return new NopLogger(name);
        }
    }
}