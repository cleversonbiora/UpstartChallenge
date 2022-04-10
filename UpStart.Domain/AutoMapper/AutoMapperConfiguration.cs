
using AutoMapper;

namespace UpStart.Domain.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static IMapperConfigurationExpression RegisterMappings(IMapperConfigurationExpression conf)
        {
            conf.AddProfile(new DomainToViewModelMappingProfile());
            conf.AddProfile(new RequestToDomainMappingProfile());
            return conf;
        }
    }
}
