using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherConcurrencyApp.Domain.Entities;

namespace WeatherConcurremcyApp.AppCore.Interfaces
{
    public interface IHttpOpenWeatherClientService
    {
        Task<OpenWeather> GetWeatherByCityNameAsync(string city);
        DateTime convertToDateTime(long milisegundos);
        string GetImageLocation(Weather w);
    }
}
