using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yarish.University.Filmark.Core.Interfaces;
using Yarish.University.Filmark.Core.Services;
using Yarish.University.Filmark.Models.Configuration;

namespace Yarish.University.Filmark.Core
{
    public static class DIConfiguration
    {
        public static void RegisterCoreDependencies(this IServiceCollection services)
        {
            services.AddTransient<IWeatherForecastService, WeatherForecastService>();
        }

        public static void RegisterCoreConfiguration(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.Configure<AppConfig>(configuration.GetSection("AppConfig"));
        }
    }
}
