using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Builders
{
    public class WalletItemModelBuilder : ICreateWalletItemModelAdded, IWalletItemModelAdded
    {
        private ICollection<CreateWalletItemModel> _createWalletItemModels;
        private ICollection<WalletItemModel> _walletItemModels;

        private ICollection<CreateWalletItemModel> CreateWalletItemModels
        {
            get => _createWalletItemModels ??= new List<CreateWalletItemModel>();

            set => _createWalletItemModels = value;
        }

        public ICollection<WalletItemModel> WalletItemModels
        {
            get => _walletItemModels ??= new List<WalletItemModel>();

            set => _walletItemModels = value;
        }

        private CreateWalletItemModel CreateWalletItemModel(string extTicker, Guid extMarketItemGuid, decimal buyPrice, DateTime buyDate, int quantity)
        {
            var createWalletItemModel = new CreateWalletItemModel
            {
                BuyPrice = buyPrice,
                BuyDate = buyDate,
                Quantity = quantity,
                ExtTicker = extTicker,
                ExtMarketItemGuid = extMarketItemGuid
            };

            return createWalletItemModel;
        }

        private WalletItemModel UpdateWalletItemModel(string extTicker, Guid extMarketItemGuid, decimal buyPrice, DateTime buyDate, int quantity, decimal amount, DateTime dateUpdated)
        {
            var walletModel = new WalletItemModel
            {
                Guid = Guid.NewGuid(),
                BuyPrice = buyPrice,
                BuyDate = buyDate,
                Quantity = quantity,
                Amount = amount,
                DateUpdated = dateUpdated,
                ExtTicker = extTicker,
                ExtMarketItemGuid = extMarketItemGuid
            };

            return walletModel;
        }

        public ICreateWalletItemModelAdded AddCreateWalletItemModel(string extTicker, Guid extMarketItemGuid, decimal buyPrice, DateTime buyDate, int quantity)
        {
            var walletModel = CreateWalletItemModel(extTicker, extMarketItemGuid, buyPrice, buyDate, quantity);

            CreateWalletItemModels.Add(walletModel);

            return this;
        }

        public IWalletItemModelAdded AddUpdateWalletItemModel(string extTicker, Guid extMarketItemGuid, decimal buyPrice, DateTime buyDate, int quantity, decimal amount, DateTime dateUpdated)
        {
            var walletModel = UpdateWalletItemModel(extTicker, extMarketItemGuid, buyPrice, buyDate, quantity, amount, dateUpdated);

            WalletItemModels.Add(walletModel);

            return this;
        }
        public IWalletItemModelAdded AddUpdateWalletItemModel(string extTicker, Guid extMarketItemGuid, decimal amount)
        {
            var walletModel = UpdateWalletItemModel(extTicker, extMarketItemGuid, Decimal.One, DateTime.Now, 1, amount, DateTime.Now);

            WalletItemModels.Add(walletModel);

            return this;
        }

        ICollection<CreateWalletItemModel> ICreateWalletItemModelAdded.Build()
        {
            var result = CreateWalletItemModels;
            CreateWalletItemModels = null;

            return result;
        }

        public ICollection<WalletItemModel> Build()
        {
            var result = WalletItemModels;
            WalletItemModels = null;

            return result;
        }
    }

    public interface ICreateWalletItemModelBuilder
    {
        ICreateWalletItemModelAdded AddCreateWalletItemModel(string ticker, Guid extMarketItemGuid, decimal buyPrice, DateTime buyDate, int quantity);
    }

    public interface IWalletItemModelBuilder
    {
        IWalletItemModelAdded AddUpdateWalletItemModel(string ticker, Guid extMarketItemGuid, decimal amount);
        IWalletItemModelAdded AddUpdateWalletItemModel(string ticker, Guid extMarketItemGuid, decimal buyPrice, DateTime buyDate, int quantity, decimal amount, DateTime dateUpdated);
    }

    public interface ICreateWalletItemModelAdded : ICreateWalletItemModelBuilder
    {
        ICollection<CreateWalletItemModel> Build();
    }

    public interface IWalletItemModelAdded : IWalletItemModelBuilder
    {
        ICollection<WalletItemModel> Build();
    }
}
