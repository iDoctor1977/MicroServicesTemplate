using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Models.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.WalletItem;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.MapperProfiles
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<CreateWalletAppDto, CreateWalletModel>().ReverseMap();
            CreateMap<CreateWalletAppDto, WalletModel>().ReverseMap();

            CreateMap<CreateWalletModel, WalletAggregate>().ReverseMap();
            CreateMap<WalletModel, WalletAggregate>()
                .ForMember(x => x.WalletItems, opt => { opt.Ignore(); })
                .ReverseMap();

            CreateMap<CreateWalletItemModel, WalletItemEntity>().ReverseMap();
            CreateMap<WalletItemModel, WalletItemEntity>().ReverseMap();
            CreateMap<WalletItemAppDto, WalletItemModel>().ReverseMap();

            CreateMap<CreateWalletEventApiDto, CreateWalletEventAppDto>().ReverseMap();
            CreateMap<CreateWalletEventAppDto, CreateWalletEventModel>().ReverseMap();
        }
    }
}
