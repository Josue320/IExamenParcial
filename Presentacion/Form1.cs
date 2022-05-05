using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherConcurremcyApp.AppCore.Interfaces;
using WeatherConcurrencyApp;
using WeatherConcurrencyApp.Common;
using WeatherConcurrencyApp.Domain.Entities;


namespace Presentacion
{
    public partial class Form1 : Form
    {
        private IHttpOpenWeatherClientService httpOpenWeatherClient;
        private IWeatherRepositoryService weatherRepository;
        private ForeCastInfo forecast;
        private OpenWeather openWeather;
        public double x, y;

        public Form1(IHttpOpenWeatherClientService httpOpenWeatherClient, IWeatherRepositoryService weatherRepository)
        {
            this.httpOpenWeatherClient = httpOpenWeatherClient;
            this.weatherRepository = weatherRepository;
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Run(Request).Wait();
                Task.Run(Request2).Wait();
                if (forecast == null)
                {
                    throw new NullReferenceException("Fallo al obtener el objeto OpeWeather.");
                }
                WeatherPanel weatherPanel = new WeatherPanel();
                AñadirDatos(weatherPanel);
                flpDatos.Controls.Add(weatherPanel);
                InfoPanel info;
                for (int i = 0; i < 9; i++)
                {
                    info = new InfoPanel();
                    info.label6.Text = httpOpenWeatherClient.convertToDateTime(forecast.hourly[i].dt).ToShortTimeString(); ;
                    info.label8.Text = forecast.hourly[i].weather[0].Main;
                    info.label9.Text = forecast.hourly[i].weather[0].Description;
                    info.label7.Text = forecast.hourly[i].wind_speed.ToString();
                    info.label10.Text = forecast.hourly[i].pressure.ToString();
                    info.picClima.ImageLocation = $"{AppSettings.ApiIcon}" + forecast.hourly[i].weather[0].Icon + ".png";
                    flpDatos.Controls.Add(info);
                }


            }
            catch (Exception)
            {

            }
        }

        public async Task Request()
        {
            openWeather = await httpOpenWeatherClient.GetWeather(txtCiudad.Text);
           
        }
        public async Task Request2()
        {
            x = openWeather.Coord.Lat;
            y = openWeather.Coord.Lon;
            forecast = await httpOpenWeatherClient.GetWeatherByCityNameAsync(y, x);

        }

        public void AñadirDatos(WeatherPanel weatherPanel)

        {
            weatherRepository.Add(openWeather);
            weatherPanel.lblCity.Text = openWeather.Name;

            weatherPanel.lblTemperature.Text = openWeather.Main.Temp.ToString() + "°C"; ;
            weatherPanel.lblWeather.Text = openWeather.Weather[0].Main.ToString();
            weatherPanel.pictureBox1.ImageLocation = httpOpenWeatherClient.GetImageLocation(openWeather.Weather[0]);
            weatherPanel.d1.lblDetailValue.Text = openWeather.Main.Temp_max.ToString() + "°C";
            weatherPanel.d2.lblDetailValue.Text = openWeather.Main.Temp_min.ToString() + "°C";
            weatherPanel.d3.lblDetailValue.Text = openWeather.Main.Humidity.ToString();
            weatherPanel.d4.lblDetailValue.Text = openWeather.Sys.Country.ToString();
            weatherPanel.d5.lblDetailValue.Text = openWeather.Main.Pressure.ToString();
            weatherPanel.d6.lblDetailValue.Text = openWeather.Wind.Speed.ToString() + "km/h";
            weatherPanel.d7.lblDetailValue.Text = httpOpenWeatherClient.convertToDateTime(openWeather.Sys.Sunrise).ToShortTimeString();
            weatherPanel.d8.lblDetailValue.Text = httpOpenWeatherClient.convertToDateTime(openWeather.Sys.Sunset).ToShortTimeString();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }
    }
}
