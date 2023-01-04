using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;

namespace CoreServicesTemplate.Dashboard.Web.MapperProfiles
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<UserViewModel, UserAppModel>().ReverseMap();
            CreateMap<UsersViewModel, UsersAppModel>().ReverseMap();
        }
    }
}
