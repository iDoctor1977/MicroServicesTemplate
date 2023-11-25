using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models.AppModels.Wallets;
using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;

namespace CoreServicesTemplate.Dashboard.Core.MapperProfiles
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<CreateWalletAppModel, WalletModel>().ReverseMap();
            CreateMap<WalletAppModel, WalletModel>().ReverseMap();
        }
    }
}
