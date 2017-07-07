//-----------------------------------------------------------------------
// <copyright file="SubDevice.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    /// <summary>
    /// Class definition of SubDevice
    /// </summary>
    public class SubDevice
    {
        /// <summary>
        /// The number of access attempts matching the device and account hash in the last hour.
        /// </summary>
        public int alh { get; set; }

        /// <summary>
        /// The number of access attempts matching the device and account hash in the last minute.
        /// </summary>
        public int alm { get; set; }

        /// <summary>
        /// The number of access attempts matching the device and ip address hash in the last hour.
        /// </summary>
        public int iplh { get; set; }

        /// <summary>
        /// The number of access attempts matching the device and ip address hash in the last minute.
        /// </summary>
        public int iplm { get; set; }

        /// <summary>
        /// The number of access attempts matching the device and password hash in the last hour.
        /// </summary>
        public int plh { get; set; }

        /// <summary>
        /// The number of access attempts matching the device and password hash in the last minute.
        /// </summary>
        public int plm { get; set; }

        /// <summary>
        /// The number of access attempts matching the device and username hash in the last hour.
        /// </summary>
        public int ulh { get; set; }

        /// <summary>
        /// The number of access attempts matching the device and username hash in the last minute.
        /// </summary>
        public int ulm { get; set; }
    }
}