using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;

public interface IGetWalletItemsEfDepot : IQueryHandler<Guid, ICollection<WalletItemModel>> { }