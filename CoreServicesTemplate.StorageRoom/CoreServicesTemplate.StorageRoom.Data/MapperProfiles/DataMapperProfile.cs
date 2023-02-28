using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.AppModels;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.MapperProfiles
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<UserAppModel, User>().ReverseMap();
        }
    }
}
