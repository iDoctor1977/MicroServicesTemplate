using AutoMapper;
using CoreServicesTemplate.Bus.Common.Models;
using CoreServicesTemplate.Shared.Core.Models.Wallet;

namespace CoreServicesTemplate.Bus.Services.MapperProfiles
{
    public class ServiceMapperProfile : Profile
    {
        public ServiceMapperProfile()
        {
            CreateMap<ResponseEmailPropertiesApiDto, EmailPropertiesModel>().ReverseMap();
        }
    }
}
