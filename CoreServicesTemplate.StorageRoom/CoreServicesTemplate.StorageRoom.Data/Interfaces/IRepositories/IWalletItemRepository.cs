using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;

public interface IWalletItemRepository : IRepository<WalletItem>
{
    Task<IEnumerable<WalletItem>> ReadWalletItemsByOwnerGuidAsync(Guid ownerGuid);
}