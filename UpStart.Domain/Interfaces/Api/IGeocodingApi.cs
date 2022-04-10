using Refit;
using System;
using System.Threading.Tasks;
using UpStart.Domain.Models.Geocoding;

namespace UpStart.Domain.Interfaces.Api
{
    public interface IGeocodingApi
    {
        [Get("/geocoder/locations/onelineaddress?address={address}&benchmark=2020&format=json")]
        Task<LocationResult> GetLocation(string address);

    }
}
