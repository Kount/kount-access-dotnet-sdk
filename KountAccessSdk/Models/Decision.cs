//-----------------------------------------------------------------------
// <copyright file="Decision.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class definition of Decision
    /// </summary>
    public class Decision
    {
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }

        [JsonProperty("reply")]
        public Reply Reply { get; set; }

        [JsonProperty("warnings")]
        public List<string> Warnings { get; set; }
    }
}