using HW6_final.DTO;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HW6_final
{
    class Requests
    {
        public Task<JsonAlmanac> AlmanacQuery(string country, string city)
        {
            Task<JsonAlmanac> t1 = Task.Factory.StartNew(() =>
            {
                string url = String.Format("http://api.wunderground.com/api/c0e27010fab4b60b/almanac/q/{0}/{1}.json", country, city);
                var result = new WebClient().DownloadString(url);
                return JsonConvert.DeserializeObject<JsonAlmanac>(result);

            });
            return t1;
        }

        public Task<JsonConditions> ConditionsQuery(string country, string city)
        {
            Task<JsonConditions> t2 = Task.Factory.StartNew(() =>
            {
                string url = String.Format("http://api.wunderground.com/api/c0e27010fab4b60b/conditions/q/{0}/{1}.json", country, city);
                var result = new WebClient().DownloadString(url);
                return JsonConvert.DeserializeObject<JsonConditions>(result);
            });
            return t2;
        }

        public Task<JsonForecast> Forecast3Query(string country, string city)
        {
            Task<JsonForecast> t3 = Task.Factory.StartNew(() =>
            {
                string url = String.Format("http://api.wunderground.com/api/c0e27010fab4b60b/forecast/q/{0}/{1}.json", country, city);
                var result = new WebClient().DownloadString(url);
                return JsonConvert.DeserializeObject<JsonForecast>(result);
            });
            return t3;
        }
    }
}
