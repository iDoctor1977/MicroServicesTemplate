using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;

public class WalletModelBase : IModelBase
{
    public Guid OwnerGuid { get; set; }
    public decimal Balance { get; set; }
    public decimal TradingAllowedBalance { get; set; }
    public decimal OperationAllowedBalance { get; set; }
}