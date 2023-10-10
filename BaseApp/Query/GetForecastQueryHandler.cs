using BaseApp.DataAccess;
using BaseApp.Models;
using Mediator;

namespace BaseApp.Query
{
    public class GetForecastQueryHandler : IRequestHandler<GetForecastQuery, IEnumerable<WeatherForecast>>
    {
        private readonly IRepository<WeatherForecast> _forecastsRepository;

        public GetForecastQueryHandler(IRepository<WeatherForecast> forecastsRepository)
        {
            _forecastsRepository = forecastsRepository;
        }

        public async ValueTask<IEnumerable<WeatherForecast>> Handle(GetForecastQuery request, CancellationToken cancellationToken)
        {
            return await _forecastsRepository.GetAllAsync().ConfigureAwait(false);
        }
    }
}
