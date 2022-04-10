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
    public class LocationController : BaseController
    {

        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string address)
        {
            return Response(await _locationService.GetLocation(address));
        }
    }
}
