﻿using CoreServicesTemplate.StorageRoom.Common.DomainModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;

public class WalletModel : BaseWalletModel
{
    public Guid Guid { get; set; }
    public decimal Performance { get; set; }
    public ICollection<WalletItemModel> WalletItems { get; set; }
}