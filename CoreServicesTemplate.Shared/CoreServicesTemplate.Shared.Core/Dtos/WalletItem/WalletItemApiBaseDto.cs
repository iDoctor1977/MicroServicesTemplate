using System;

namespace CoreServicesTemplate.Shared.Core.Dtos.WalletItem;

public class WalletItemApiBaseDto
{
    public decimal? Amount { get; set; }
    public decimal? BuyPrice { get; set; }
    public DateTime? BuyDate { get; set; }
    public int? Quantity { get; set; }
    public DateTime? DateUpdated { get; set; }
}