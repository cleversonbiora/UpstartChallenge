using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpStart.CrossCutting;
using UpStart.Domain.AutoMapper;
using UpStart.Domain.Interfaces.Api;
using UpStart.Domain.Interfaces.Service;
using UpStart.Domain.ViewModels.Weather;

namespace UpStart.Application.Service.Services
{
    public class WeatherService : BaseService, IWeatherService
    {

        protected readonly IWeatherApi _weatherApi;

        public WeatherService(
                IWeatherApi weatherApi
            )
        {
            _weatherApi = weatherApi;
        }

        public async Task<IEnumerable<ForecastResultVM>> GetForecast(float latitude, float longitude)
        {
            var point = _weatherApi.GetPoints(latitude, longitude);
            return (await _weatherApi.GetForecast(point.Result.properties.gridId, point.Result.properties.gridX, point.Result.properties.gridY)).properties.periods.Select(x => Mapper.Map<ForecastResultVM>(x)) ;
        }
    }
}
 