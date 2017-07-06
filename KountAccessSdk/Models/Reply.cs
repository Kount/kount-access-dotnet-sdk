//-----------------------------------------------------------------------
// <copyright file="Reply.cs" company="Kount Inc">
//     Copyright 2017 Kount Inc. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class definition of Reply
    /// </summary>
    public class Reply
    {
        [JsonProperty("ruleEvents")]
        public RuleEvents RuleEvents { get; set; }
    }
}