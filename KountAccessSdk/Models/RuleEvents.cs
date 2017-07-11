//-----------------------------------------------------------------------
// <copyright file="RuleEvents.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class definition of RuleEvents
    /// </summary>
    public class RuleEvents
    {
        [JsonProperty("decision")]
        public string Decision { get; set; }

        [JsonProperty("ruleEvents")]
        public List<string> Events { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}