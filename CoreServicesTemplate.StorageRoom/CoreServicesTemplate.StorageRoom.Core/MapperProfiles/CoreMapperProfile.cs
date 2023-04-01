using AutoMapper;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.MapperProfiles
{
    public class CoreMapperProfile : Profile
    {
        public CoreMapperProfile()
        {
            CreateMap<CreateNewWalletAppDto, CreateWalletModel>().ReverseMap();
            CreateMap<CreateNewWalletAppDto, WalletModel>().ReverseMap();

            CreateMap<CreateWalletModel, WalletAggregate>().ReverseMap();
            CreateMap<WalletModel, WalletAggregate>()
                .ForMember(x => x.WalletItems, opt => { opt.Ignore(); })
                .ReverseMap();

            CreateMap<CreateWalletItemModel, WalletItemEntity>().ReverseMap();
            CreateMap<WalletItemModel, WalletItemEntity>().ReverseMap();
            CreateMap<ResponseWalletItemsAppDto, WalletItemModel>().ReverseMap();
        }
    }
}
