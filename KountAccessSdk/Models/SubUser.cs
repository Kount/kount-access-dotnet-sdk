//-----------------------------------------------------------------------
// <copyright file="SubUser.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    /// <summary>
    /// Class definition of SubUser
    /// </summary>
    public class SubUser
    {
        /// <summary>
        /// The number of access attempts matching the username hash and account hash in the last hour.
        /// </summary>
        public int alh { get; set; }

        /// <summary>
        /// The number of access attempts matching the username hash and account hash in the last minute.
        /// </summary>
        public int alm { get; set; }

        /// <summary>
        /// The number of access attempts matching the username hash and device hash in the last hour.
        /// </summary>
        public int dlh { get; set; }

        /// <summary>
        /// The number of access attempts matching the username hash and device hash in the last minute.
        /// </summary>
        public int dlm { get; set; }

        /// <summary>
        /// The number of access attempts matching the username hash and ipAddress hash in the last hour.
        /// </summary>
        public int iplh { get; set; }

        /// <summary>
        /// The number of access attempts matching the username hash and ipAddress hash in the last minute.
        /// </summary>
        public int iplm { get; set; }

        /// <summary>
        /// The number of access attempts matching the username hash and password hash in the last hour.
        /// </summary>
        public int plh { get; set; }

        /// <summary>
        /// The number of access attempts matching the username hash and password hash in the last minute.
        /// </summary>
        public int plm { get; set; }

        public override bool Equals(object obj)
        {
            SubUser sp = obj as SubUser;

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

                if (sp.plh != plh)
                {
                    return false;
                }

                if (sp.plm != plm)
                {
                    return false;
                }

                return true;
            }
            return false;
        }

        public override int GetHashCode()
        {
            int sum = alh + alm + dlh + dlm + iplh + iplm + plh + plm;
            return sum.GetHashCode();
        }
    }
}