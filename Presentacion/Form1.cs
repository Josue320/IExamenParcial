using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherConcurremcyApp.AppCore.Interfaces;
using WeatherConcurrencyApp;
using WeatherConcurrencyApp.Domain.Entities;


namespace Presentacion
{
    public partial class Form1 : Form
    {
        private IHttpOpenWeatherClientService httpOpenWeatherClient;
        private IWeatherRepositoryService weatherRepository;
        private OpenWeather openWeather;

        
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

                if (openWeather == null )
                {
                    throw new NullReferenceException("Fallo al obtener el objeto OpeWeather.");
                }

                WeatherPanel weatherPanel = new WeatherPanel();
                AñadirDatos(weatherPanel);

                flpDatos.Controls.Add(weatherPanel);
            }
            catch (Exception)
            {

            }
        }

        public async Task Request()
        {
            openWeather = await httpOpenWeatherClient.GetWeatherByCityNameAsync("managua");
            
        }

        public void AñadirDatos(WeatherPanel weatherPanel)

        {
            weatherRepository.Add(openWeather);
            weatherPanel.lblCity.Text = openWeather.Name;

            weatherPanel.lblTemperature.Text = openWeather.Main.Temp.ToString() + "°C"; ;
            weatherPanel.lblWeather.Text = openWeather.Weather[0].Main.ToString();
            // pictureBox1.ImageLocation = service.GetImageLocation(infoClima.weather[0]);
            weatherPanel.pictureBox1.ImageLocation = httpOpenWeatherClient.GetImageLocation(openWeather.Weather[0]);
            weatherPanel.d1.lblDetail.Text = "Temperatura maxima";
            weatherPanel.d1.lblDetailValue.Text = openWeather.Main.Temp_max.ToString() + "°C";
            weatherPanel.d2.lblDetail.Text = "Temperatura minima";
            weatherPanel.d2.lblDetailValue.Text = openWeather.Main.Temp_min.ToString() + "°C";
            weatherPanel.d3.lblDetail.Text = "Humedad";
            weatherPanel.d3.lblDetailValue.Text = openWeather.Main.Humidity.ToString();
            weatherPanel.d4.lblDetail.Text = "Pais";
            weatherPanel.d4.lblDetailValue.Text = openWeather.Sys.Country.ToString();
            weatherPanel.d5.lblDetail.Text = "Presion";
            weatherPanel.d5.lblDetailValue.Text = openWeather.Main.Pressure.ToString();
            weatherPanel.d6.lblDetail.Text = "Velocidad viento";
            weatherPanel.d6.lblDetailValue.Text = openWeather.Wind.Speed.ToString() + "km/h";
            weatherPanel.d7.lblDetail.Text = "Amanecer";
            weatherPanel.d7.lblDetailValue.Text = httpOpenWeatherClient.convertToDateTime(openWeather.Sys.Sunrise).ToShortTimeString();
            weatherPanel.d8.lblDetail.Text = "Atardecer";
            weatherPanel.d8.lblDetailValue.Text = httpOpenWeatherClient.convertToDateTime(openWeather.Sys.Sunset).ToShortTimeString();

            //

        }
    }
}
