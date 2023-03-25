using AutoMapper;
using CoreServicesTemplate.Shared.Core.Dtos.Wallet;
using CoreServicesTemplate.Shared.Core.Dtos.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Api.MapperProfiles
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<CreateWalletApiDto, CreateWalletAppDto>().ReverseMap();
            CreateMap<WalletItemApiDto, WalletItemAppDto>().ReverseMap();
        }
    }
}
