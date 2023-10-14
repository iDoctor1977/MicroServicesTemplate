﻿namespace CoreServicesTemplate.Dashboard.Common.Models.Wallets;

public class CreateWalletModelBase
{
    public decimal Balance { get; set; }
    public decimal TradingAllowedBalance { get; set; }
    public decimal OperationAllowedBalance { get; set; }
}