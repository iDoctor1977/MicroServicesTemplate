using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Api.MapperProfiles
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<CreateWalletDto, CreateWalletAppDto>().ReverseMap();
            CreateMap<WalletItemDto, WalletItemAppDto>().ReverseMap();
        }
    }
}
