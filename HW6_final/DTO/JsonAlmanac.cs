using Newtonsoft.Json;
using System;

namespace HW6_final.DTO
{
    class Almanac
    {
        [JsonProperty("temp_high")]
        public AlmanacTemp highestTemp { get; set; }

        [JsonProperty("temp_low")]
        public AlmanacTemp lowestTemp { get; set; }
    }

    class AlmanacTemp
    {
        [JsonProperty("normal")]
        public AlmanacTempType normal { get; set; }

        [JsonProperty("record")]
        public AlmanacTempType record { get; set; }

        [JsonProperty("recordyear")]
        public int year { get; set; }
    }

    class AlmanacTempType
    {
        [JsonProperty("C")]
        public int c { get; set; }
    }

    class JsonAlmanac
    {
        [JsonProperty("almanac")]
        public Almanac almanac { get; set; }
    }
}
