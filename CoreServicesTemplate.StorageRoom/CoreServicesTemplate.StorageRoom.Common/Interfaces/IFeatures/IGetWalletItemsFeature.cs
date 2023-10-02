using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models.WalletItem;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;

public interface IGetWalletItemsFeature : IQueryHandler<Guid, ICollection<WalletItemAppDto>> { }