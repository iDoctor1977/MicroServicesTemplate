namespace CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;

public class WalletItemModelBase
{
    public decimal BuyPrice { get; set; }
    public DateTime BuyDate { get; set; }
    public int Quantity { get; set; }

    public string ExtTicker { get; set; }
    public Guid ExtMarketItemGuid { get; set; }
}