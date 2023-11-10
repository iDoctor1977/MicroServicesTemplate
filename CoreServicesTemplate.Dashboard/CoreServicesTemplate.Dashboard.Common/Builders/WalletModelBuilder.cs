using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;

namespace CoreServicesTemplate.Dashboard.Common.Builders
{
    public class WalletModelBuilder : IWalletModelBuilder, IWalletModelAdded
    {
        private ICollection<WalletModel> _walletModelCollection;

        public WalletModelBuilder()
        {
            _walletModelCollection = new List<WalletModel>();
        }

        private WalletModel CreateWallet(Guid ownerGuid, decimal? balance, decimal? tradingAllowedBalance, decimal? operationAllowedBalance)
        {
            var wallet = new WalletModel
            {
                OwnerGuid = ownerGuid,
                Balance = balance,
                TradingAllowedBalance = tradingAllowedBalance,
                OperationAllowedBalance = operationAllowedBalance
            };

            return wallet;
        }

        public IWalletModelBuilder AddWallet(Guid ownerGuid, decimal? balance, decimal? tradingAllowedBalance, decimal? operationAllowedBalance)
        {
            var wallet = CreateWallet(ownerGuid, balance, tradingAllowedBalance, operationAllowedBalance);

            _walletModelCollection.Add(wallet);

            return this;
        }

        public ICollection<WalletModel> Build()
        {
            var result = _walletModelCollection;
            _walletModelCollection = null;

            return result;
        }
    }

    public interface IWalletModelAdded
    {
        IWalletModelBuilder AddWallet(Guid ownerGuid, decimal? balance, decimal? tradingAllowedBalance, decimal? operationAllowedBalance);
    }

    public interface IWalletModelBuilder
    {
        ICollection<WalletModel> Build();
    }
}
