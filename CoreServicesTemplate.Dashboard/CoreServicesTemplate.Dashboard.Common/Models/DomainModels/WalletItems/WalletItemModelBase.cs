using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Dashboard.Common.Models.DomainModels.WalletItems;

public class WalletItemModelBase : IModelBase
{
    public decimal? Amount { get; set; }
    public decimal? BuyPrice { get; set; }
    public DateTime? BuyDate { get; set; }
    public int? Quantity { get; set; }
    public DateTime? DateUpdated { get; set; }
}