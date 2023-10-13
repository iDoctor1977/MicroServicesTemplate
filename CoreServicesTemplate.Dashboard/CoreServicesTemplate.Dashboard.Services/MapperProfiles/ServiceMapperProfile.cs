using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models.WalletItems;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Models.WalletItem;

namespace CoreServicesTemplate.Dashboard.Services.MapperProfiles
{
    public class ServiceMapperProfile : Profile
    {
        public ServiceMapperProfile()
        {
            CreateMap<CreateWalletAppModel, CreateWalletApiDto>().ReverseMap();
            CreateMap<WalletModel, WalletApiDto>().ReverseMap();

            CreateMap<WalletItemModel, MarketItemApiDto>().ReverseMap();
        }
    }
}
