﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherConcurrencyApp.Domain.Entities;

namespace WeatherConcurrencyApp.Domain.Interfaces
{
    public interface IHttpOpenWeatherClient
    {
        Task<OpenWeather> GetWeatherByCityNameAsync(string city);
        DateTime convertToDateTime(long milisegundos);
        string GetImageLocation(Weather w);
    }
}
