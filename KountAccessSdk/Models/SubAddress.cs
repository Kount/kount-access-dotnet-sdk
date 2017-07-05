using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

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
