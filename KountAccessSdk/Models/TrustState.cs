//-----------------------------------------------------------------------
// <copyright file="TrustState.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;
    using KountAccessSdk.Enums;

    /// <summary>
    /// Class definition of TrustState model
    /// </summary>
    public class TrustState
    {
        [JsonProperty("state")]
        public DeviceTrustState State { get; set; }
    }
}
