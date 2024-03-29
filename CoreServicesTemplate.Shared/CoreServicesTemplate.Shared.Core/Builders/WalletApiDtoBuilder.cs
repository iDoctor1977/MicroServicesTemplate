﻿using System;
using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Models.Wallet;

namespace CoreServicesTemplate.Shared.Core.Builders
{
    public class WalletApiDtoBuilder : IWalletApiDtoBuilder, IWalletApiDtoAdded
    {
        private ICollection<ResponseStorageRoomGetWalletApiDto> _walletApiDtoCollection;

        public WalletApiDtoBuilder()
        {
            _walletApiDtoCollection = new List<ResponseStorageRoomGetWalletApiDto>();
        }

        private ResponseStorageRoomGetWalletApiDto CreateWallet(Guid ownerGuid, decimal? balance, decimal? tradingAllowedBalance, decimal? operationAllowedBalance)
        {
            var wallet = new ResponseStorageRoomGetWalletApiDto
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

        public ICollection<ResponseStorageRoomGetWalletApiDto> Build()
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
        ICollection<ResponseStorageRoomGetWalletApiDto> Build();
    }
}
