using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.MappingProfiles
{
    public class FeatureMappingProfile : Profile
    {
        public FeatureMappingProfile()
        {
            CreateMap<UserAppModel, UserAggModel>().ReverseMap();
            CreateMap<AddressAppModel, AddressAggModel>().ReverseMap();
        }
    }
}
