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
        public int alh { get; set; }
        public int alm { get; set; }
        public int dlh { get; set; }
        public int dlm { get; set; }
        public int iplh { get; set; }
        public int iplm { get; set; }
        public int ulh { get; set; }
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