using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.JSON;

namespace WeatherApp.Model.DATA
{
  
    public class Data
    {
        public string cityname;

        public async Task<LocationData> GetLocationFromIp()
        {
            string IpApiUrl = "https://ep.api.getfastah.com/whereis/v1/json/auto?fastah-key=638eec4613e64929aa0a4d7505b6d8cb";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage  response = await client.GetAsync(IpApiUrl);
                response.EnsureSuccessStatusCode();
                string json = await response.Content.ReadAsStringAsync();
                Root1? data = JsonConvert.DeserializeObject<Root1>(json);
                cityname = data.locationData.cityName;
                return data.locationData; 
            }

        }

        public async Task<Root> GetWeatherData()
        {
            var result = await GetLocationFromIp();
            string URL = $"https://api.weatherbit.io/v2.0/forecast/daily?&lat={result.lat}&lon={result.lng}&key=a658f93700ff430d982412310d58a72e";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(URL);
                response.EnsureSuccessStatusCode(); 
                string json = await response.Content.ReadAsStringAsync();
                Root? data = JsonConvert.DeserializeObject<Root>(json);

                return data;
            }
        }
    }
}
