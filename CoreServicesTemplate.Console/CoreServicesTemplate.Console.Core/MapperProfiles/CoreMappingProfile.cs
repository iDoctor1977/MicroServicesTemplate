using AutoMapper;
using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Core.MapperProfiles
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
