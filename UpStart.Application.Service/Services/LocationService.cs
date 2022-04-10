using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpStart.CrossCutting;
using UpStart.Domain.AutoMapper;
using UpStart.Domain.Interfaces.Api;
using UpStart.Domain.Interfaces.Service;
using UpStart.Domain.ViewModels.Location;

namespace UpStart.Application.Service.Services
{
    public class LocationService : BaseService, ILocationService
    {
        protected readonly IGeocodingApi _geocodingApi;

        public LocationService(
                IGeocodingApi geocodingApi
            )
        {
            _geocodingApi = geocodingApi;
        }

        public async Task<IEnumerable<AddressResultVM>> GetLocation(string address)
        {
            var locationResult = await _geocodingApi.GetLocation(address);
            return locationResult.result.addressMatches.Select(x => Mapper.Map<AddressResultVM>(x));
        }
    }
}
 