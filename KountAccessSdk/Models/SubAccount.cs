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
        public int dlh { get; set; }
        public int dlm { get; set; }
        public int iplh { get; set; }
        public int iplm { get; set; }
        public int plh { get; set; }
        public int plm { get; set; }
        public int ulh { get; set; }
        public int ulm { get; set; }
    }
}