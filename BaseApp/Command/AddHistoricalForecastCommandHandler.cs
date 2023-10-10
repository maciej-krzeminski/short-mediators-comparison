using BaseApp.DataAccess;
using BaseApp.Models;
using Mediator;

namespace BaseApp.Command
{
    public class AddHistoricalForecastCommandHandler : IRequestHandler<AddHistoricalForecastCommand>
    {
        private readonly IRepository<WeatherForecast> _forecastsRepository;

        public AddHistoricalForecastCommandHandler(IRepository<WeatherForecast> forecastsRepository)
        {
            _forecastsRepository = forecastsRepository;
        }

        public async ValueTask<Unit> Handle(AddHistoricalForecastCommand request, CancellationToken cancellationToken)
        {
            await _forecastsRepository.AddAsync(request.NewHistoricalForecast).ConfigureAwait(false);
            
            return Unit.Value;
        }
    }
}
