﻿namespace CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet
{
    public class CreateWalletAppModel : WalletAppModelBase
    {
        public decimal TradingAllowedBalance { get; set; }
        public decimal OperationAllowedBalance { get; set; }
        public decimal Balance { get; set; }
    }
}
