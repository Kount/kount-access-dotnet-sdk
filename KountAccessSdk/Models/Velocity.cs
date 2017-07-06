//-----------------------------------------------------------------------
// <copyright file="Velocity.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class definition of Velocity
    /// </summary>
    public class Velocity
    {
        [JsonProperty("account")]
        public SubAccount Account { get; set; }

        [JsonProperty("device")]
        public SubDevice Device { get; set; }

        [JsonProperty("ip_address")]
        public SubAddress IpAddress { get; set; }

        [JsonProperty("password")]
        public SubPassword Password { get; set; }

        [JsonProperty("user")]
        public SubUser User { get; set; }
    }
}