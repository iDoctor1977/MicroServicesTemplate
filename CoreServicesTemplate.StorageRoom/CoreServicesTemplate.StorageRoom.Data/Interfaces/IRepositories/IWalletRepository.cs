using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;

public interface IWalletRepository : IRepository<Wallet>
{
    Task<IEnumerable<WalletItem>> ReadWalletItemsByOwnerGuidAsync(Guid ownerGuid);
    Task<Wallet?> ReadForOwnerGuidAsync(Guid ownerGuid);
}