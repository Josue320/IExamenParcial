using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherConcurrencyApp.Domain.Entities
{
    public class City
    {
        public float Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Coord coord { get; set; }
        public class Coord
        {
            public double Lon { get; set; }
            public double Lat { get; set; }
        }
    }
}
