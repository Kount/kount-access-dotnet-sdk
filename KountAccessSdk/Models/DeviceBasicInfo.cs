//-----------------------------------------------------------------------
// <copyright file="DeviceBasicInfo.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using KountAccessSdk.Enums;
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Class definition of DeviceBasicInfo model
    /// </summary>
    public class DeviceBasicInfo
    {
        [JsonProperty("deviceid")]
        public string DeviceId { get; set; }

        [JsonProperty("truststate")]
        public DeviceTrustState TrustState { get; set; }

        [JsonProperty("datefirstseen")]
        public DateTime DateFirstSeen { get; set; }

        [JsonProperty("friendlyname")]
        public string FriendlyName { get; set; }
    }
}
