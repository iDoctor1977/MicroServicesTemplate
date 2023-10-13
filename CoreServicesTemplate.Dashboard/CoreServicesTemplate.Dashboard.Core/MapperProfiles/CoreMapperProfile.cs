using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;

namespace CoreServicesTemplate.Dashboard.Core.MapperProfiles
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<CreateWalletAppModel, CreateWalletModel>().ReverseMap();
            CreateMap<WalletAppModel, WalletModel>().ReverseMap();
        }
    }
}
