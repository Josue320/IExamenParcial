using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherConcurrencyApp.Common;
using WeatherConcurrencyApp.Domain.Entities;
using WeatherConcurrencyApp.Domain.Interfaces;

namespace WeatherConcurrencyApp.Infraestructura.OpenWeatherClient
{
    public class HttpOpenWeatherClient : IHttpOpenWeatherClient
    {
        public async Task<ForeCastInfo> GetWeatherByCityNameAsync(double lon, double lat)
        {

            long dt = DateTimeOffset.Now.ToUnixTimeSeconds();
            string url = $"{AppSettings.ApiUrl}lat={lat}&lon={lon}&dt={dt}&appid={AppSettings.Token}";
            string jsonObject = string.Empty;
            
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    jsonObject = await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
                }

                if (string.IsNullOrEmpty(jsonObject))
                {
                    throw new NullReferenceException("El objeto json no puede ser null.");
                }

                return Newtonsoft.Json.JsonConvert.DeserializeObject<ForeCastInfo>(jsonObject);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public DateTime convertToDateTime(long milisegundos)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(milisegundos).ToLocalTime();
            return day;
        }

        public string GetImageLocation(Weather w)
        {
            string imageLocation = $@"https://openweathermap.org/img/w/{w.Icon}.png";
            return imageLocation;
        }

        public async Task<OpenWeather> GetWeather(string city)
        {
            string ApiUrl = "https://api.openweathermap.org/data/2.5/weather?q=";
            string url = $"{ApiUrl}{city}&units={AppSettings.Units}&lang=sp&appid={AppSettings.Token}";
            string jsonObject = string.Empty;
            try
            {
                
                using (HttpClient httpClient = new HttpClient())
                {
                    
                    jsonObject = await httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();
                }

                if (string.IsNullOrEmpty(jsonObject))
                {
                    throw new NullReferenceException("El objeto json no puede ser null.");
                }

                return Newtonsoft.Json.JsonConvert.DeserializeObject<OpenWeather>(jsonObject);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
