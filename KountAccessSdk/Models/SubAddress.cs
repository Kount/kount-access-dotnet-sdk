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
        public int alh { get; set; }
        public int alm { get; set; }
        public int dlh { get; set; }
        public int dlm { get; set; }
        public int plh { get; set; }
        public int plm { get; set; }
        public int ulh { get; set; }
        public int ulm { get; set; }
    }
}