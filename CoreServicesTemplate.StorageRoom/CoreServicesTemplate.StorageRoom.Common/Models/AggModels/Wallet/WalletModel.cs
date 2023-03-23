﻿using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;

public class WalletModel : BaseWalletModel
{
    public Guid Guid { get; set; }
    public decimal Performance { get; set; }
    public ICollection<WalletItemModel> WalletItems { get; set; }
}