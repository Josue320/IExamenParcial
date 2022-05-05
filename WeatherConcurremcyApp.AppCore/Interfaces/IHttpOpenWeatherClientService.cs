﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherConcurrencyApp.Domain.Entities;

namespace WeatherConcurremcyApp.AppCore.Interfaces
{
    public interface IHttpOpenWeatherClientService
    {
        Task<ForeCastInfo> GetWeatherByCityNameAsync(double lon, double lat);
        DateTime convertToDateTime(long milisegundos);
        string GetImageLocation(Weather w);
        Task<OpenWeather> GetWeather(string city);
    }
}
