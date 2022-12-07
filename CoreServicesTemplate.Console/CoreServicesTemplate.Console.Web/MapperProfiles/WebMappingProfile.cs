using AutoMapper;
using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Console.Web.Models;

namespace CoreServicesTemplate.Console.Web.MapperProfiles
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<UserViewModel, UserModel>().ReverseMap();
            CreateMap<UsersViewModel, UsersModel>().ReverseMap();
        }
    }
}
