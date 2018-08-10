//-----------------------------------------------------------------------
// <copyright file="BehavioSec.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class definition of BehavioSec model
    /// </summary>
    public class BehavioSec
    {
        [JsonProperty("isBot")]
        public bool IsBot { get; set; }

        [JsonProperty("isTrained")]
        public bool IsTrained { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("confidence")]
        public int Confidence { get; set; }

        [JsonProperty("policyId")]
        public int PolicyId { get; set; }
    }
}
