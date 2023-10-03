﻿namespace CoreServicesTemplate.StorageRoom.Common.Models.WalletItem;

public class WalletItemAppDto
{
    public Guid GuId { get; set; }
    public decimal? Amount { get; set; }
    public decimal? BuyPrice { get; set; }
    public DateTime? BuyDate { get; set; }
    public int Quantity { get; set; }
    public DateTime? DateUpdated { get; set; }
}