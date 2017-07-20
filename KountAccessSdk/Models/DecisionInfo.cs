//-----------------------------------------------------------------------
// <copyright file="DecisionInfo.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class definition of DecisionInfo
    /// </summary>
    public class DecisionInfo
    {
        [JsonProperty("decision")]
        public Decision Decision { get; set; }

        [JsonProperty("device")]
        public Device Device { get; set; }

        [JsonProperty("response_id")]
        public string ResponseId { get; set; }

        [JsonProperty("velocity")]
        public Velocity Velocity { get; set; }
    }
}