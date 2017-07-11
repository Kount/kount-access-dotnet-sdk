//-----------------------------------------------------------------------
// <copyright file="SubAccount.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    /// <summary>
    /// Class definition of SubAccount
    /// </summary>
    public class SubAccount
    {
        /// <summary>
        /// The number of access attempts matching the account hash and device in the last hour.
        /// </summary>
        public int dlh { get; set; }

        /// <summary>
        /// The number of access attempts matching the account hash and device in the last minute.
        /// </summary>
        public int dlm { get; set; }

        /// <summary>
        ///  The number of access attempts matching the account hash and IP address in the last hour.
        /// </summary>
        public int iplh { get; set; }

        /// <summary>
        ///  The number of access attempts matching the account hash and IP address in the last minute.
        /// </summary>
        public int iplm { get; set; }

        /// <summary>
        /// The number of access attempts matching the account hash and password hash in the last hour.
        /// </summary>
        public int plh { get; set; }

        /// <summary>
        /// The number of access attempts matching the account hash and password hash in the last minute.
        /// </summary>
        public int plm { get; set; }

        /// <summary>
        /// The number of access attempts matching the account hash and username hash in the last hour.
        /// </summary>
        public int ulh { get; set; }

        /// <summary>
        /// The number of access attempts matching the account hash and username hash in the last minute.
        /// </summary>
        public int ulm { get; set; }
    }
}