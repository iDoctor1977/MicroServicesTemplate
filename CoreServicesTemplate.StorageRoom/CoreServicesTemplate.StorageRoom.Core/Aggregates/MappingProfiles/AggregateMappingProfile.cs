using AutoMapper;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregate;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.MappingProfiles
{
    public class AggregateMappingProfile : Profile
    {
        public AggregateMappingProfile()
        {
            CreateMap<UserAggModel, UserAggregate.UserAggregate>().ReverseMap();
            CreateMap<AddressItem, AddressAggModel>().ReverseMap();
        }
    }
}
