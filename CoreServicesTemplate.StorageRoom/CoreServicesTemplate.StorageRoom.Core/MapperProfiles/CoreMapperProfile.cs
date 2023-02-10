using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregates;

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
