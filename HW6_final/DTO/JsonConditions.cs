using Newtonsoft.Json;
using System;

namespace HW6_final.DTO
{
    class Conditions
    {
        [JsonProperty("temp_c")]
        public string temp { get; set; }

        [JsonProperty("relative_humidity")]
        public string humidity { get; set; }

        [JsonProperty("weather")]
        public string weather { get; set; }

        [JsonProperty("wind_dir")]
        public string windDirection { get; set; }

        [JsonProperty("wind_kph")]
        public string windSpeed { get; set; }

        [JsonProperty("visibility_km")]
        public string visibility { get; set; }

        [JsonProperty("station_id")]
        public string idStation { get; set; }
    }

    class JsonConditions
    {
        [JsonProperty("current_observation")]
        public Conditions conditions { get; set; }
    }
}
