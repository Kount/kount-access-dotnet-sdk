namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    public class Reply
    {
        [JsonProperty("ruleEvents")]
        public RuleEvents RuleEvents { get; set; }
    }
}