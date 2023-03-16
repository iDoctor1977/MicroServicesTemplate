using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Api.MapperProfiles
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<CreateWalletApiModel, CreateWalletAppDto>().ReverseMap();
            CreateMap<WalletItemApiModel, WalletItemAppDto>().ReverseMap();
        }
    }
}
