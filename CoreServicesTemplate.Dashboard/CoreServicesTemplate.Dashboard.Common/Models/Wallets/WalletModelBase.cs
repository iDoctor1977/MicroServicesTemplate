namespace CoreServicesTemplate.Dashboard.Common.Models.Wallets;

public class WalletModelBase
{
    public Guid OwnerGuid { get; set; }
    public decimal? TradingAllowedBalance { get; set; }
    public decimal? OperationAllowedBalance { get; set; }
    public decimal? Balance { get; set; }
}