using BaseApp.Command;
using BaseApp.DataAccess;
using BaseApp.Models;
using BaseApp.Query;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SystemFile = System.IO.File;

namespace BaseApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {            
            return await _mediator.Send(new GetForecastQuery()).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<IActionResult> Post(WeatherForecast newHistoricalForecast)
        {
            await _mediator.Send(new AddHistoricalForecastCommand(newHistoricalForecast)).ConfigureAwait(false);

            return Ok();
        }
    }
}