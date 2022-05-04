using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherConcurremcyApp.AppCore.Interfaces;
using WeatherConcurremcyApp.AppCore.Services;
using WeatherConcurrencyApp.Domain.Interfaces;
using WeatherConcurrencyApp.Infraestructura.OpenWeatherClient;

namespace Presentacion
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();
            builder.RegisterType<HttpOpenWeatherClient>().As<IHttpOpenWeatherClient>();
            builder.RegisterType<HttpOpenWeatherClientService>().As<IHttpOpenWeatherClientService>();
            var container = builder.Build();

            Application.Run(new Form1(container.Resolve<IHttpOpenWeatherClientService>()));
        }
    }
}
