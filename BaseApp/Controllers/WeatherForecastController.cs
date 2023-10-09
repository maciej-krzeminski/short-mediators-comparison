using BaseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SystemFile = System.IO.File;

namespace BaseApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly string _historicalForecastPath = Path.Combine(Path.GetTempPath(), "historicalForecasts.json");
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            var generatedForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                Type = ForecastType.Generated
            });

            return generatedForecasts.Concat(await GetHistoricalForecasts().ConfigureAwait(false)).ToArray();
        }

        [HttpPost]
        public async Task<IActionResult> Post(WeatherForecast newHistoricalForecast)
        {
            await AddNewHistoricalForecast(newHistoricalForecast).ConfigureAwait(false);

            return Ok();
        }

        private async Task<IEnumerable<WeatherForecast>> GetHistoricalForecasts()
        {
            if (!SystemFile.Exists(_historicalForecastPath))
            {
                return new List<WeatherForecast>();
            }

            try
            {
                string json = await SystemFile.ReadAllTextAsync(_historicalForecastPath).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<WeatherForecast>>(json)!;
            }
            catch (Exception)
            {
                return new List<WeatherForecast>();
            }
        }

        private async Task AddNewHistoricalForecast(WeatherForecast newHistoricalForecast)
        {
            newHistoricalForecast.Type = ForecastType.Historical;
            newHistoricalForecast.Date = DateTime.Now.AddDays(-1);
            var historicalForecasts = (await GetHistoricalForecasts().ConfigureAwait(false)).ToList();
            historicalForecasts.Add(newHistoricalForecast);

            await SystemFile.WriteAllTextAsync(_historicalForecastPath, JsonConvert.SerializeObject(historicalForecasts)).ConfigureAwait(false);
        }
    }
}