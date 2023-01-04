using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.MapperProfiles
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<UserAppModel, UserApiModel>().ReverseMap();
            CreateMap<UsersAppModel, UsersApiModel>().ReverseMap();
        }
    }
}
