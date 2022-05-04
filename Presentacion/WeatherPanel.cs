using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherConcurrencyApp
{
    public partial class WeatherPanel : UserControl
    {
        public DetailsWeather d1 = new DetailsWeather();
        public DetailsWeather d2 = new DetailsWeather();
        public DetailsWeather d3 = new DetailsWeather();
        public DetailsWeather d4 = new DetailsWeather();
        public DetailsWeather d5 = new DetailsWeather();
        public DetailsWeather d6 = new DetailsWeather();
        public DetailsWeather d7 = new DetailsWeather();
        public DetailsWeather d8 = new DetailsWeather();
        public WeatherPanel()
        {
            InitializeComponent();
           
            
            flpContent.Controls.Add(d1);
            flpContent.Controls.Add(d2);
            flpContent.Controls.Add(d3);
            flpContent.Controls.Add(d4);
            flpContent.Controls.Add(d5);
            flpContent.Controls.Add(d6);
            flpContent.Controls.Add(d7);
            flpContent.Controls.Add(d8);

        }

       
    }
}
