namespace CoreServicesTemplate.StorageRoom.Common.DomainModels.WalletItem;

public class BaseWalletItemModel
{
    public decimal BuyPrice { get; set; }
    public DateTime BuyDate { get; set; }
    public int Quantity { get; set; }

    public string ExtTicker { get; set; }
    public Guid ExtMarketItemGuid { get; set; }
}