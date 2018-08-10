//-----------------------------------------------------------------------
// <copyright file="Info.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class definition of Info
    /// </summary>
    public class Info : DecisionInfo
    {
        [JsonProperty("trusted")]
        public TrustState Trusted { get; set; }

        [JsonProperty("behavioSec")]
        public BehavioSec BehavioSec { get; set; }
    }
}
