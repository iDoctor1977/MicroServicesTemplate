using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Builders
{
    public class WalletModelBuilder : ICreateWalletModelAdded, IWalletModelAdded
    {
        private ICollection<CreateWalletModel> _createWalletModels;
        private ICollection<WalletModel> _walletModels;

        private ICollection<CreateWalletModel> CreateWalletModels
        {
            get => _createWalletModels ??= new List<CreateWalletModel>();

            set => _createWalletModels = value;
        }

        public ICollection<WalletModel> WalletModels
        {
            get => _walletModels ??= new List<WalletModel>();

            set => _walletModels = value;
        }

        public CreateWalletModel CreateWalletModel(
            Guid ownerGuid, 
            decimal balance, 
            decimal tradingAllowedBalance,
            decimal operationAllowedBalance)
        {
            var createWalletModel = new CreateWalletModel()
            {
                OwnerGuid = ownerGuid,
                Balance = balance,
                TradingAllowedBalance = tradingAllowedBalance,
                OperationAllowedBalance = operationAllowedBalance
            };

            return createWalletModel;
        }

        public WalletModel UpdateWalletModel(
            Guid ownerGuid, 
            decimal balance,
            decimal tradingAllowedBalance,
            decimal operationAllowedBalance, 
            decimal performance,
            ICollection<WalletItemModel> walletItems)
        {
            var walletModel = new WalletModel
            {
                Guid = Guid.NewGuid(),
                OwnerGuid = ownerGuid,
                Balance = balance,
                TradingAllowedBalance = tradingAllowedBalance,
                OperationAllowedBalance = operationAllowedBalance,
                Performance = performance,
                WalletItems = walletItems
            };

            return walletModel;
        }

        public ICreateWalletModelAdded AddCreateWalletModel(Guid ownerGuid, decimal balance, decimal tradingAllowedBalance,
            decimal operationAllowedBalance)
        {
            var createWalletModel = CreateWalletModel(ownerGuid, balance, tradingAllowedBalance, operationAllowedBalance);

            CreateWalletModels.Add(createWalletModel);

            return this;
        }

        public IWalletModelAdded AddUpdateWalletModel(Guid ownerGuid, decimal balance, decimal tradingAllowedBalance,
            decimal operationAllowedBalance, decimal performance, ICollection<WalletItemModel> walletItems)
        {
            var walletModel = UpdateWalletModel(ownerGuid, balance, tradingAllowedBalance, operationAllowedBalance, performance, walletItems);

            WalletModels.Add(walletModel);

            return this;
        }
        public IWalletModelAdded AddUpdateWalletModel(Guid ownerGuid, decimal tradingAllowedBalance, ICollection<WalletItemModel> walletItems)
        {
            var walletModel = UpdateWalletModel(ownerGuid, Decimal.One, tradingAllowedBalance, decimal.One, Decimal.One, walletItems);

            WalletModels.Add(walletModel);

            return this;
        }

        ICollection<CreateWalletModel> ICreateWalletModelAdded.Build()
        {
            var result = CreateWalletModels;
            CreateWalletModels = null;

            return result;
        }

        public ICollection<WalletModel> Build()
        {
            var result = WalletModels;
            WalletModels = null;

            return result;
        }
    }

    public interface ICreateWalletModelBuilder
    {
        ICreateWalletModelAdded AddCreateWalletModel(
            Guid ownerGuid,
            decimal balance,
            decimal tradingAllowedBalance,
            decimal operationAllowedBalance);
    }

    public interface IWalletModelBuilder
    {
        IWalletModelAdded AddUpdateWalletModel(
            Guid ownerGuid, 
            decimal tradingAllowedBalance,
            ICollection<WalletItemModel> walletItems);

        IWalletModelAdded AddUpdateWalletModel(
            Guid ownerGuid,
            decimal balance,
            decimal tradingAllowedBalance,
            decimal operationAllowedBalance,
            decimal performance,
            ICollection<WalletItemModel> walletItems);
    }

    public interface ICreateWalletModelAdded : ICreateWalletModelBuilder
    {
        ICollection<CreateWalletModel> Build();
    }
    public interface IWalletModelAdded : IWalletModelBuilder
    {
        ICollection<WalletModel> Build();
    }
}
