using AutoMapper;
using UpStart.Domain.Models.Geocoding;
using UpStart.Domain.ViewModels.Location;
using UpStart.Domain.ViewModels.Weather;
using static UpStart.Domain.Models.Weather.ForecastResult;

namespace UpStart.Domain.AutoMapper
{
    class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Addressmatch, AddressResultVM>()
              .ForMember(dest => dest.MatchedAddress, map => map.MapFrom(src => src.matchedAddress))
              .ForMember(dest => dest.Longitude, map => map.MapFrom(src => src.coordinates.x))
              .ForMember(dest => dest.Latitude, map => map.MapFrom(src => src.coordinates.y))
              .ForMember(dest => dest.FromAddress, map => map.MapFrom(src => src.addressComponents.fromAddress))
              .ForMember(dest => dest.ToAddress, map => map.MapFrom(src => src.addressComponents.toAddress))
              .ForMember(dest => dest.StreetName, map => map.MapFrom(src => src.addressComponents.streetName))
              .ForMember(dest => dest.SuffixType, map => map.MapFrom(src => src.addressComponents.suffixType))
              .ForMember(dest => dest.City, map => map.MapFrom(src => src.addressComponents.city))
              .ForMember(dest => dest.State, map => map.MapFrom(src => src.addressComponents.state))
              .ForMember(dest => dest.Zip, map => map.MapFrom(src => src.addressComponents.zip));
            CreateMap<Period, ForecastResultVM>();
            
        }
    }
}

