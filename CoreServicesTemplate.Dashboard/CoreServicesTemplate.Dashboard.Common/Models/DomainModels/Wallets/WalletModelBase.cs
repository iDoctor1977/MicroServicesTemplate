using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;

public class WalletModelBase : IModelBase
{
    public Guid OwnerGuid { get; set; }
    public decimal? TradingAllowedBalance { get; set; }
    public decimal? OperationAllowedBalance { get; set; }
    public decimal? Balance { get; set; }
}