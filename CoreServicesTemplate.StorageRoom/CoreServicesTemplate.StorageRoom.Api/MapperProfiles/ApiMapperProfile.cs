using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.MapperProfiles
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<AddressAppModel, AddressApiModel>().ReverseMap();
            CreateMap<UserAppModel, UserApiModel>().ReverseMap();
            CreateMap<UsersAppModel, UsersApiModel>().ReverseMap();
        }
    }
}
