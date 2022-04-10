using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpStart.Domain.Interfaces.Service;

namespace UpStart.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : BaseController
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(float latitude, float longitude)
        {
            return Response(await _weatherService.GetForecast(latitude, longitude));
        }
    }
}
