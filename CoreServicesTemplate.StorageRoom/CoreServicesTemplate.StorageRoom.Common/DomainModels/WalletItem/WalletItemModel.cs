﻿namespace CoreServicesTemplate.StorageRoom.Common.DomainModels.WalletItem;

public class WalletItemModel : WalletItemModelBase
{
    public Guid Guid { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateUpdated { get; set; }
}