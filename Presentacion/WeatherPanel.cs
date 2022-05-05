using Presentacion;
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

            d1.lblDetail.Text = "Temperatura maxima";
            d2.lblDetail.Text = "Temperatura minima";
            d3.lblDetail.Text = "Humedad";
            d4.lblDetail.Text = "Pais";
            d5.lblDetail.Text = "Presion";
            d6.lblDetail.Text = "Velocidad viento";
            d7.lblDetail.Text = "Amanecer";
            d8.lblDetail.Text = "Atardecer";

        }
    }
}
