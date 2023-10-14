using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Models.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;

public interface IGetWalletItemsEfDepot : IQueryHandler<Guid, ICollection<WalletItemModel>> { }