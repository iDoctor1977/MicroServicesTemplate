using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.MapperProfiles
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<UserAppModel, UserApiModel>().ReverseMap();
            CreateMap<UsersAppModel, UsersApiModel>().ReverseMap();
        }
    }
}
