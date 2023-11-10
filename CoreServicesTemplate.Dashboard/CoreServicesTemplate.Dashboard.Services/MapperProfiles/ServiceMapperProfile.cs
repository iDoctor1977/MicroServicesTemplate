using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.WalletItems;
using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Models.WalletItem;

namespace CoreServicesTemplate.Dashboard.Services.MapperProfiles
{
    public class ServiceMapperProfile : Profile
    {
        public ServiceMapperProfile()
        {
            CreateMap<WalletModel, RequestStorageRoomCreateWalletApiDto>().ReverseMap();
            CreateMap<WalletModel, ResponseStorageRoomGetWalletApiDto>().ReverseMap();

            CreateMap<WalletItemModel, MarketItemApiDto>().ReverseMap();
        }
    }
}
