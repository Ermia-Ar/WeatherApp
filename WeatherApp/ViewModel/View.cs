using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.JSON;
using WeatherApp.Model.DATA;

namespace WeatherApp.ViewModel
{
    public class View
    {
        public string cityname;
        private Data data;


        public View()
        {
            data = new Data();
        }   

        public async Task<List<Datum>> GetInfo()
        {
            var result = await data.GetWeatherData();
            cityname = data.cityname;

            return result.data;
        }
    }
}
