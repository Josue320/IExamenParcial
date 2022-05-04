using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherConcurrencyApp.Domain.Entities;
using WeatherConcurrencyApp.Domain.Interfaces;

namespace WeatherConcurrencyApp.Infraestructura.Repository
{
    public class WeatherRepository : IWeatherRepository
    {
        public RAFFContext context;
        public const int SIZE = 300;
        public WeatherRepository()
        {
            context = new RAFFContext("Weather", SIZE);
        }
        public void Add(OpenWeather t)
        {
            try
            {
                context.Create<OpenWeather>(t);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(OpenWeather t)
        {

            try
            {
                return context.Delete<OpenWeather>(t);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public OpenWeather GetById(int id)
        {

            try
            {
                return context.Get<OpenWeather>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<OpenWeather> Read()
        {

            try
            {
                return context.GetAll<OpenWeather>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update(OpenWeather t)
        {
            try
            {
                return context.Update<OpenWeather>(t);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
