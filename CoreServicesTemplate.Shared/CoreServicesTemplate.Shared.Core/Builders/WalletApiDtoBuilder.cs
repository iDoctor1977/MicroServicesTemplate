using System;
using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Models.Wallet;

namespace CoreServicesTemplate.Shared.Core.Builders
{
    public class WalletApiDtoBuilder : IWalletApiDtoBuilder, IWalletApiDtoAdded
    {
        private ICollection<ResponseWalletApiDto> _walletApiDtoCollection;

        public WalletApiDtoBuilder()
        {
            _walletApiDtoCollection = new List<ResponseWalletApiDto>();
        }

        private ResponseWalletApiDto CreateWallet(Guid ownerGuid, decimal? balance, decimal? tradingAllowedBalance, decimal? operationAllowedBalance)
        {
            var wallet = new ResponseWalletApiDto
            {
                OwnerGuid = ownerGuid,
                Balance = balance,
                TradingAllowedBalance = tradingAllowedBalance,
                OperationAllowedBalance = operationAllowedBalance
            };

            return wallet;
        }

        public IWalletApiDtoBuilder AddWallet(Guid ownerGuid, decimal? balance, decimal? tradingAllowedBalance, decimal? operationAllowedBalance)
        {
            var wallet = CreateWallet(ownerGuid, balance, tradingAllowedBalance, operationAllowedBalance);

            _walletApiDtoCollection.Add(wallet);

            return this;
        }

        public ICollection<ResponseWalletApiDto> Build()
        {
            var result = _walletApiDtoCollection;
            _walletApiDtoCollection = null;

            return result;
        }
    }

    public interface IWalletApiDtoAdded
    {
        IWalletApiDtoBuilder AddWallet(Guid ownerGuid, decimal? balance, decimal? tradingAllowedBalance, decimal? operationAllowedBalance);
    }

    public interface IWalletApiDtoBuilder
    {
        ICollection<ResponseWalletApiDto> Build();
    }
}
