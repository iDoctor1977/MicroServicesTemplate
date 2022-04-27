using CoreServicesTemplate.Console.Web.Models;

namespace CoreServicesTemplate.Console.Web.MapperProfiles
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<UserViewModel, UserApiModel>().ReverseMap();
        }
    }
}
