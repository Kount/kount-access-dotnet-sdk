//-----------------------------------------------------------------------
// <copyright file="DevicesInfo.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class definition of DeviceInfo model
    /// </summary>
    public class DevicesInfo : KountResponseInfo
    {
        [JsonProperty("devices")]
        public List<DeviceBasicInfo> Devices { get; set; }
    }
}