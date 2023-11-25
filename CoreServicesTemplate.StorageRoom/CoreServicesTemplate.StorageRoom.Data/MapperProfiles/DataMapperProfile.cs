using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.MapperProfiles
{
    public class DataMapperProfile : Profile
    {
        public DataMapperProfile()
        {

            CreateMap<WalletModel, Wallet>().ReverseMap();
            CreateMap<WalletItemModel, WalletItem>().ReverseMap();
            CreateMap<EmailPropertiesModel, Wallet>().ReverseMap();
        }
    }
}
