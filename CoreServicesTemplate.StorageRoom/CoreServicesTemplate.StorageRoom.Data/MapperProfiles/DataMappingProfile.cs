using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.MapperProfiles
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<User, UserApiModel>().ReverseMap();
        }
    }
}
