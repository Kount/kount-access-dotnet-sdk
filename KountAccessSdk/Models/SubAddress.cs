//-----------------------------------------------------------------------
// <copyright file="SubAddress.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    /// <summary>
    /// Class definition of SubAddress
    /// </summary>
    public class SubAddress
    {
        /// <summary>
        /// The number of access attempts matching the IP address and account hash in the last hour.
        /// </summary>
        public int alh { get; set; }

        /// <summary>
        /// The number of access attempts matching the IP address and account hash in the last minute.
        /// </summary>
        public int alm { get; set; }

        /// <summary>
        /// The number of access attempts matching the IP address and device hash in the last hour.
        /// </summary>
        public int dlh { get; set; }

        /// <summary>
        /// The number of access attempts matching the IP address and device hash in the last minute.
        /// </summary>
        public int dlm { get; set; }

        /// <summary>
        /// The number of access attempts matching the IP address and password hash in the last hour.
        /// </summary>
        public int plh { get; set; }

        /// <summary>
        /// The number of access attempts matching the IP address and password hash in the last minute.
        /// </summary>
        public int plm { get; set; }

        /// <summary>
        /// The number of access attempts matching the IP address and username hash in the last hour.
        /// </summary>
        public int ulh { get; set; }

        /// <summary>
        /// The number of access attempts matching the IP address and username hash in the last minute.
        /// </summary>
        public int ulm { get; set; }
    }
}