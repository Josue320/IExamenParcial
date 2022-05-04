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
    public class WeatherRepositoryService : IWeatherRepositoryService
    {
        public IWeatherRepository weatherRepository;
        public WeatherRepositoryService(IWeatherRepository weatherRepository)
        {
            this.weatherRepository = weatherRepository;
        }
        public void Add(OpenWeather t)
        {
            weatherRepository.Add(t);
        }

        public bool Delete(OpenWeather t)
        {
            throw new NotImplementedException();
        }

        public OpenWeather GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<OpenWeather> Read()
        {
            return weatherRepository.Read();
        }

        public int Update(OpenWeather t)
        {
            throw new NotImplementedException();
        }
    }
}
