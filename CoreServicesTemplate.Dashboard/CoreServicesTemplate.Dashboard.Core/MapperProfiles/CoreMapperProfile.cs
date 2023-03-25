using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Dtos.Wallet;

namespace CoreServicesTemplate.Dashboard.Core.MapperProfiles
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<CreateWalletAppModel, CreateWalletApiDto>().ReverseMap();
            CreateMap<WalletAppModel, WalletApiDto>().ReverseMap();
        }
    }
}
