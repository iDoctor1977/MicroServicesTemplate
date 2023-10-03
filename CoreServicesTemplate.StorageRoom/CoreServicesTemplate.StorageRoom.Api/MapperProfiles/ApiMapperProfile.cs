using AutoMapper;
using CoreServicesTemplate.Shared.Core.DtoModels.Wallet;
using CoreServicesTemplate.Shared.Core.DtoModels.WalletItem;
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
        }
    }
}
