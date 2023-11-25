using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CoreServicesTemplate.StorageRoom.Data.CustomMappers
{
    public class WalletDataCustomMapper : CustomMapperBase<WalletModel, Wallet>
    {
        private readonly IDefaultMapper<WalletItemModel, WalletItem> _walletItemMapper;

        public WalletDataCustomMapper(
            IDefaultMapper<WalletModel, Wallet> defaultMapper, 
            IDefaultMapper<WalletItemModel, WalletItem> walletItemMapper) : base(defaultMapper)
        {
            _walletItemMapper = walletItemMapper;
        }

        public override WalletModel Map(Wallet valueIn)
        {
            var walletModel = base.Map(valueIn);

            if (walletModel.WalletItems.IsNullOrEmpty())
            {
                walletModel.WalletItems = new List<WalletItemModel>();
            }

            walletModel.WalletItems = _walletItemMapper.Map(valueIn.ColWalletItems);

            return walletModel;
        }
    }
}
