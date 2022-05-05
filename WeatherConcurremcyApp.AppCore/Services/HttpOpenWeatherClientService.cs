using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherConcurremcyApp.AppCore.Interfaces;
using WeatherConcurrencyApp.Domain.Entities;
using WeatherConcurrencyApp.Domain.Interfaces;

namespace WeatherConcurremcyApp.AppCore.Services
{
    public class HttpOpenWeatherClientService : IHttpOpenWeatherClientService
    {
        public IHttpOpenWeatherClient httpOpenWeatherClient;
        public HttpOpenWeatherClientService(IHttpOpenWeatherClient openWeatherClient)
        {
            this.httpOpenWeatherClient = openWeatherClient;
        }
        public DateTime convertToDateTime(long milisegundos)
        {
            return httpOpenWeatherClient.convertToDateTime(milisegundos);
        }

        public string GetImageLocation(Weather w)
        {
            return httpOpenWeatherClient.GetImageLocation(w);
        }

        public Task<OpenWeather> GetWeather(string city)
        {
            return httpOpenWeatherClient.GetWeather(city);
        }

        public Task<ForeCastInfo> GetWeatherByCityNameAsync(double lon, double lat )
        {
            return httpOpenWeatherClient.GetWeatherByCityNameAsync(lon,lat);
        }
    }
}
