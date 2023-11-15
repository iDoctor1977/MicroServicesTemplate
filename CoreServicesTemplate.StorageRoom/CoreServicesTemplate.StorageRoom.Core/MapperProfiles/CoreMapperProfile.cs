using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Domain.Entities;

namespace CoreServicesTemplate.StorageRoom.Core.MapperProfiles
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<CreateWalletAppModel, CreateWalletModel>().ReverseMap();
            CreateMap<CreateWalletAppModel, WalletModel>().ReverseMap();

            CreateMap<CreateWalletModel, Wallet>().ReverseMap();
            CreateMap<WalletModel, Wallet>()
                .ForMember(x => x.WalletItems, opt => { opt.Ignore(); })
                .ReverseMap();

            CreateMap<CreateWalletItemModel, WalletItem>().ReverseMap();
            CreateMap<WalletItemModel, WalletItem>().ReverseMap();
            CreateMap<WalletItemAppModel, WalletItemModel>().ReverseMap();

            CreateMap<ResponseStorageRoomEmailPropertiesApiDto, EmailPropertiesAppModel>().ReverseMap();
            CreateMap<EmailPropertiesAppModel, EmailPropertiesModel>().ReverseMap();
        }
    }
}
