//-----------------------------------------------------------------------
// <copyright file="UniquesInfo.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class definition of UniquesInfo model
    /// </summary>
    public class UniquesInfo : KountResponseInfo
    {
        [JsonProperty("uniques")]
        public List<Unique> Uniques { get; set; }
    }
}
