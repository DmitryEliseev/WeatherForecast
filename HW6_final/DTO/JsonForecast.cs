using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HW6_final.DTO
{
    class JsonForecast
    {
        [JsonProperty("forecast")]
        public Forecast forecast { get; set; }
    }

    class Forecast
    {
        [JsonProperty("txt_forecast")]
        public ForecastDay txt_forecast { get; set; }
    }

    class ForecastDay
    {
        [JsonProperty("forecastday")]
        public List<ForecastDayData> forecastDay { get; set; }
    }

    class ForecastDayData
    {
        [JsonProperty("fcttext_metric")]
        public string description { get; set; }

        [JsonProperty("icon_url")]
        public string icon { get; set; }

        [JsonProperty("title")]
        public string day { get; set; }
    }
}
