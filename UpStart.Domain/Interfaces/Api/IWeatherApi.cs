using Refit;
using System;
using System.Threading.Tasks;
using UpStart.Domain.Models.Weather;

namespace UpStart.Domain.Interfaces.Api
{
    public interface IWeatherApi
    {
        [Get("/points/{latitude},{longitude}")]
        Task<PointsResult> GetPoints(float latitude, float longitude);

        [Get("/gridpoints/{gridId}/{gridX},{gridY}/forecast")]
        Task<ForecastResult> GetForecast(string gridId, int gridX, int gridY);

    }
}
