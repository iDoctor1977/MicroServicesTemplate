using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;

public interface IGetWalletItemsDepot : IQueryHandler<Guid, ICollection<WalletItemModel>> { }