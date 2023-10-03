using System;

namespace CoreServicesTemplate.Shared.Core.Models.Wallet
{
    public class WalletApiBaseDto
    {
        public Guid OwnerGuid { get; set; }
        public decimal? TradingAllowedBalance { get; set; }
        public decimal? OperationAllowedBalance { get; set; }
        public decimal? Balance { get; set; }
    }
}