using BaseApp.Models;
using MediatR;

namespace BaseApp.Query
{
    public record GetForecastQuery : IRequest<IEnumerable<WeatherForecast>>;
}
