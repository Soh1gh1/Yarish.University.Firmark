using Microsoft.Extensions.Options;
using Yarish.University.Filmark.Core.Constants;
using Yarish.University.Filmark.Core.Interfaces;
using Yarish.University.Filmark.Models.Configuration;
using Yarish.University.Filmark.Models.Weather;

namespace Yarish.University.Filmark.Core.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IOptions<AppConfig> _configuration;

        public WeatherForecastService(IOptions<AppConfig> configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<WeatherForecast> GetRandomForecast()
        {
            var amount = _configuration?.Value?.ForecastAmount ?? 1;

            return Enumerable.Range(1, amount).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = GetRandomSummary()
            })
            .ToArray();
        }

        private string GetRandomSummary()
        {
            return ProjectConstants.WeatherSummaries[Random.Shared.Next(ProjectConstants.WeatherSummaries.Length)];
        }
    }
}
