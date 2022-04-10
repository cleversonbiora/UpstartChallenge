using System.Collections.Generic;
using System.Threading.Tasks;
using UpStart.Domain.ViewModels.Weather;

namespace UpStart.Domain.Interfaces.Service
{
    public interface IWeatherService
    {
        Task<IEnumerable<ForecastResultVM>> GetForecast(float latitude, float longitude);
    }
}
