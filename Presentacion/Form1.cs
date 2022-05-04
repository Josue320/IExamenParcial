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
using WeatherConcurrencyApp.Domain.Entities;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        private IHttpOpenWeatherClientService httpOpenWeatherClient;
        private OpenWeather openWeather;
        
        public Form1(IHttpOpenWeatherClientService httpOpenWeatherClient)
        {
            this.httpOpenWeatherClient = httpOpenWeatherClient;
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

        }

        public async Task Request()
        {
            //openWeather = await httpOpenWeatherClient.GetWeatherByCityNameAsync(textBox1.Text);
            
        }
    }
}
