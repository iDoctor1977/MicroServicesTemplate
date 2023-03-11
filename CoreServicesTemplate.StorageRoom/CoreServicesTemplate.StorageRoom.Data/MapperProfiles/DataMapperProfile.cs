using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.MapperProfiles
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {
            CreateMap<UserAggModel, User>().ReverseMap();
        }
    }
}
