namespace CoreServicesTemplate.Dashboard.Common.Models.AppModels.WalletItems;

public class WalletItemAppModelBase
{
    public decimal Amount { get; set; }
    public decimal BuyPrice { get; set; }
    public DateTime BuyDate { get; set; }
    public int Quantity { get; set; }
    public DateTime DateUpdated { get; set; }
}