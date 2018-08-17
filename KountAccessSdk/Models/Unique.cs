//-----------------------------------------------------------------------
// <copyright file="Unique.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using KountAccessSdk.Enums;
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Class definition of Unique model
    /// </summary>
    public class Unique
    {
        [JsonProperty("unique")]
        public string UniqueId { get; set; }

        [JsonProperty("datelastseen")]
        public DateTime DateLastSeen { get; set; }

        [JsonProperty("truststate")]
        public DeviceTrustState TrustState { get; set; }
    }
}
