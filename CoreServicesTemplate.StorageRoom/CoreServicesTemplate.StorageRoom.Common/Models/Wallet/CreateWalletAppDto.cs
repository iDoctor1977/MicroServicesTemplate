﻿namespace CoreServicesTemplate.StorageRoom.Common.Models.Wallet
{
    public class CreateWalletAppDto : BaseWalletAppDto
    {
        public decimal TradingAllowedBalance { get; set; }
        public decimal OperationAllowedBalance { get; set; }
        public decimal Balance { get; set; }
    }
}
