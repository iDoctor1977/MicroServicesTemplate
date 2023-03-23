using AutoMapper;
using CoreServicesTemplate.Dashboard.Common.Models.WalletItems;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Dashboard.Web.Models.WalletItems;
using CoreServicesTemplate.Dashboard.Web.Models.Wallets;

namespace CoreServicesTemplate.Dashboard.Web.MapperProfiles
{
    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            CreateMap<CreateWalletViewModel, CreateWalletAppModel>().ReverseMap();
            CreateMap<WalletItemViewModel, WalletItemAppModel>().ReverseMap();
        }
    }
}
