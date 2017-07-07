//-----------------------------------------------------------------------
// <copyright file="SubPassword.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    /// <summary>
    /// Class definition of SubPassword
    /// </summary>
    public class SubPassword
    {
        /// <summary>
        /// The number of access attempts matching the password hash and account hash in the last hour.
        /// </summary>
        public int alh { get; set; }

        /// <summary>
        /// The number of access attempts matching the password hash and account hash in the last minute.
        /// </summary>
        public int alm { get; set; }

        /// <summary>
        /// The number of access attempts matching the password hash and device hash in the last hour.
        /// </summary>
        public int dlh { get; set; }

        /// <summary>
        /// The number of access attempts matching the password hash and device hash in the last minute.
        /// </summary>
        public int dlm { get; set; }

        /// <summary>
        /// The number of access attempts matching the password hash and ipAddress hash in the last hour.
        /// </summary>
        public int iplh { get; set; }

        /// <summary>
        /// The number of access attempts matching the password hash and ipAddress hash in the last minute.
        /// </summary>
        public int iplm { get; set; }

        /// <summary>
        /// The number of access attempts matching the password hash and username hash in the last hour.
        /// </summary>
        public int ulh { get; set; }

        /// <summary>
        /// The number of access attempts matching the password hash and username hash in the last minute.
        /// </summary>
        public int ulm { get; set; }

        public override bool Equals(object obj)
        {
            SubPassword sp = obj as SubPassword;

            if (sp != null)
            {
                if (sp.alh != alh)
                {
                    return false;
                }

                if (sp.alm != alm)
                {
                    return false;
                }

                if (sp.dlh != dlh)
                {
                    return false;
                }

                if (sp.dlm != dlm)
                {
                    return false;
                }

                if (sp.iplh != iplh)
                {
                    return false;
                }

                if (sp.iplm != iplm)
                {
                    return false;
                }

                if (sp.ulh != ulh)
                {
                    return false;
                }

                if (sp.ulm != ulm)
                {
                    return false;
                }

                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int sum = alh + alm + dlh + dlm + iplh + iplm + ulh + ulm;
            return sum.GetHashCode();
        }
    }
}