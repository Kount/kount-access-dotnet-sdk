namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    public class DeviceInfo
    {

        [JsonProperty("device")]
        public Device Device { get; set; }

        [JsonProperty("response_id")]
        public string ResponceId { get; set; }
    }
}
