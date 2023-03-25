namespace CoreServicesTemplate.Dashboard.Common.Models.WalletItems;

public class WalletItemAppBaseModel
{
    public decimal Amount { get; set; }
    public decimal BuyPrice { get; set; }
    public DateTime BuyDate { get; set; }
    public int Quantity { get; set; }
    public DateTime DateUpdated { get; set; }
}