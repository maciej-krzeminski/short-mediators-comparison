﻿using BaseApp.Models;
using MediatR;

namespace BaseApp.Command
{
    public class AddHistoricalForecastCommand : IRequest
    {
        public WeatherForecast NewHistoricalForecast { get; }

        public AddHistoricalForecastCommand(WeatherForecast newHistoricalForecast)
        {
            NewHistoricalForecast = newHistoricalForecast;
        }
    }
}
