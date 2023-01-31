using AutoMapper;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.MappingProfiles
{
    public class AggregateMappingProfile : Profile
    {
        public AggregateMappingProfile()
        {
            CreateMap<UserAggModel, UserAggregate>().ReverseMap();
            CreateMap<AddressAggModel, AddressItem>().ReverseMap();
        }
    }
}
