namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class RuleEvents
    {
        [JsonProperty("decision")]
        public string Decision { get; set; }

        [JsonProperty("ruleEvents")]
        public List<string> Events { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}