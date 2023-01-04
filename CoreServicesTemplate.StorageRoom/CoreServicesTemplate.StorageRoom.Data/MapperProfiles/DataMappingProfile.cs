using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.MapperProfiles
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<UserAppModel, User>().ReverseMap();
        }
    }
}
