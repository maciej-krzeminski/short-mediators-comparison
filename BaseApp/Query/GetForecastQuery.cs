using BaseApp.Models;
using Mediator;

namespace BaseApp.Query
{
    public record GetForecastQuery : IRequest<IEnumerable<WeatherForecast>>;
}
