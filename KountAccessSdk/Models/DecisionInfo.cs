namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    public class DecisionInfo
    {
        [JsonProperty("decision")]
        public Decision Decision { get; set; }

        [JsonProperty("device")]
        public Device Device { get; set; }

        [JsonProperty("response_id")]
        public string ResponceId { get; set; }

        [JsonProperty("velocity")]
        public Velocity Velocity { get; set; }
    }
}