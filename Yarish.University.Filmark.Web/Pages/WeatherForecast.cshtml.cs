using Microsoft.AspNetCore.Mvc.RazorPages;
using Yarish.University.Filmark.Core.Interfaces;
using Yarish.University.Filmark.Models.Weather;

namespace Yarish.University.Filmark.Web.Pages
{
    public class WeatherForecastModel : PageModel
    {
        public IList<WeatherForecast> Forecasts { get; set; }

        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastModel(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        public void OnGet()
        {
            Forecasts = _weatherForecastService.GetRandomForecast().ToList();
        }
    }
}