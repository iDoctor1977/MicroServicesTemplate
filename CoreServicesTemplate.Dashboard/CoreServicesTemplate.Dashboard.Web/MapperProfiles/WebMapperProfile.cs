using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;

namespace CoreServicesTemplate.Dashboard.Web.MapperProfiles
{
    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            CreateMap<UserViewModel, UserAppModel>().ReverseMap();
            CreateMap<UsersViewModel, UsersAppModel>().ReverseMap();
        }
    }
}
