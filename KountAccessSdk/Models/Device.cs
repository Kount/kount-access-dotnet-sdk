﻿namespace KountAccessSdk.Models
{
    using Newtonsoft.Json;

    public class Device
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; }

        [JsonProperty("ipGeo")]
        public string IpGeo { get; set; }

        [JsonProperty("mobile")]
        public int Mobile { get; set; }

        [JsonProperty("proxy")]
        public int Proxy { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("geoLat")]
        public double GeoLat { get; set; }

        [JsonProperty("geoLong")]
        public double GeoLong { get; set; }

    }
}