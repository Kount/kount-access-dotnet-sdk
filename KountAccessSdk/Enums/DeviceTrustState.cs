//-----------------------------------------------------------------------
// <copyright file="DeviceTrustState.cs" company="Kount Inc">
//     Copyright 2018 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace KountAccessSdk.Enums
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime.Serialization;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DeviceTrustState
    {
        [EnumMember(Value = "trusted")]
        Trusted,

        [EnumMember(Value = "not_trusted")]
        NotTrusted,

        [EnumMember(Value = "banned")]
        Banned
    }
}
