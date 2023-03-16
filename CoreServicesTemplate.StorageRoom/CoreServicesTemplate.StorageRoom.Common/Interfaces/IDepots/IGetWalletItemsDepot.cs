using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;

public interface IGetWalletItemsDepot : ICqrsQuery<Guid, ICollection<WalletItemModel>> { }