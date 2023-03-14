using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Address;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.MapperProfiles
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<UserAppModel, UserAggModel>().ReverseMap();
            CreateMap<AddressAppModel, AddressAggModel>().ReverseMap();
            CreateMap<UserAggModel, UserAggregate>().ReverseMap();
            CreateMap<AddressAggModel, AddressAggregate>().ReverseMap();
        }
    }
}
