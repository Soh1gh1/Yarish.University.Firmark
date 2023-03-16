using Yarish.University.Filmark.Models.Weather;

namespace Yarish.University.Filmark.Core.Interfaces
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecast> GetRandomForecast();
    }
}
