namespace CoreServicesTemplate.Dashboard.Common.Models.WalletItems;

public class WalletItemModelBase
{
    public decimal? Amount { get; set; }
    public decimal? BuyPrice { get; set; }
    public DateTime? BuyDate { get; set; }
    public int? Quantity { get; set; }
    public DateTime? DateUpdated { get; set; }
}