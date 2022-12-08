using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.MapperProfiles
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<UserModel, UserApiModel>().ReverseMap();
            CreateMap<UsersModel, UsersApiModel>().ReverseMap();
        }
    }
}
