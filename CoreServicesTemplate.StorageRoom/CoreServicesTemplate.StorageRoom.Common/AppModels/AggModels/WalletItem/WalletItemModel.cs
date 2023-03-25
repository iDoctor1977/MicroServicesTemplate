namespace CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;

public class WalletItemModel : BaseWalletItemModel
{
    public Guid Guid { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateUpdated { get; set; }
}