using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Models.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Api.MapperProfiles
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<RequestStorageRoomCreateWalletApiDto, CreateWalletAppModel>().ReverseMap();
            CreateMap<MarketItemApiDto, WalletItemAppModel>().ReverseMap();

            CreateMap<ResponseStorageRoomEmailPropertiesApiDto, EmailPropertiesAppModel>().ReverseMap();
        }
    }
}
