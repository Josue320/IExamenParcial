using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherConcurrencyApp.Domain.Entities;

namespace WeatherConcurrencyApp.Domain.Interfaces
{
    public interface IWeatherRepository
    {
        void Add(OpenWeather t);
        bool Delete(OpenWeather t);
        List<OpenWeather> Read();
        int Update(OpenWeather t);
        OpenWeather GetById(int id);
    }
}
