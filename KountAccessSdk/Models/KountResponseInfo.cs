//-----------------------------------------------------------------------
// <copyright file="KountResponseInfo.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class definition of KountResponseInfo
    /// </summary>
    public class KountResponseInfo
    {
        [JsonProperty("response_id")]
        public string ResponseId { get; set; }
    }
}
