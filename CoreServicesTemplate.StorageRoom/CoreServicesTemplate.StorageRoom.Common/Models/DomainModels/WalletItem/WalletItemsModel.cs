namespace CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.WalletItem;

public class WalletItemsModel
{
    public Guid OwnerGuid { get; set; }
    public ICollection<WalletItemModel> WalletItemModels { get; set; }
}