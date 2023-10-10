using BaseApp.Models;
using Newtonsoft.Json;

namespace BaseApp.DataAccess
{
    public class WeatherForecastRepository : IRepository<WeatherForecast>
    {
        private readonly string _historicalForecastPath = Path.Combine(Path.GetTempPath(), "historicalForecasts.json");
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task AddAsync(WeatherForecast newHistoricalForecast)
        {
            await AddNewHistoricalForecast(newHistoricalForecast).ConfigureAwait(false);
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
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

        private async Task<IEnumerable<WeatherForecast>> GetHistoricalForecasts()
        {
            if (!File.Exists(_historicalForecastPath))
            {
                return new List<WeatherForecast>();
            }

            try
            {
                string json = await File.ReadAllTextAsync(_historicalForecastPath).ConfigureAwait(false);
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

            await File.WriteAllTextAsync(_historicalForecastPath, JsonConvert.SerializeObject(historicalForecasts)).ConfigureAwait(false);
        }
    }
}
