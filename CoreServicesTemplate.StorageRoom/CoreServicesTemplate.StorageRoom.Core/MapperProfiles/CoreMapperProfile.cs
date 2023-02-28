using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.AggModels;
using CoreServicesTemplate.StorageRoom.Common.AppModels;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates.UserAggregates;

namespace CoreServicesTemplate.StorageRoom.Core.MapperProfiles
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<UserAppModel, UserAggModel>().ReverseMap();
            CreateMap<AddressAppModel, AddressAggModel>().ReverseMap();
            CreateMap<UserAggModel, UserAggregate>().ReverseMap();
            CreateMap<AddressAggModel, AddressItem>().ReverseMap();
        }
    }
}
