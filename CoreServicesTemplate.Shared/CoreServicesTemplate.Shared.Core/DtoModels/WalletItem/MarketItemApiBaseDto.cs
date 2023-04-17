using System;

namespace CoreServicesTemplate.Shared.Core.DtoModels.WalletItem;

public class MarketItemApiBaseDto
{
    public decimal? Amount { get; set; }
    public decimal? BuyPrice { get; set; }
    public DateTime? BuyDate { get; set; }
    public int? Quantity { get; set; }
    public DateTime? DateUpdated { get; set; }
}