using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Models.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Models.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Api.MapperProfiles
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<CreateWalletApiDto, CreateWalletAppDto>().ReverseMap();
            CreateMap<MarketItemApiDto, WalletItemAppDto>().ReverseMap();

            CreateMap<ResponseEmailPropertiesApiDto, EmailPropertiesAppDto>().ReverseMap();
        }
    }
}
