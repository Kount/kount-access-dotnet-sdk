//-----------------------------------------------------------------------
// <copyright file="RuleEvent.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class definition of RuleEvent
    /// </summary>
    public class RuleEvent
    {
        [JsonProperty("decision")]
        public string Decision { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("expression")]
        public string Expression { get; set; }
    }
}
