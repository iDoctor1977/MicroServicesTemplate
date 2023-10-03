using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories
{
    public class WalletItemEfRepository : EfRepository<WalletItem>, IWalletItemRepository
    {
        private DbSet<WalletItem> WalletItems { get; }
        public WalletItemEfRepository(AppEfContext unitOfWorkContext) : base(unitOfWorkContext)
        {
            WalletItems = unitOfWorkContext.Set<WalletItem>();
        }
        public async Task<IEnumerable<WalletItem>> ReadWalletItemsByOwnerGuidAsync(Guid ownerGuid)
        {
            var entities = await WalletItems.Where(og => og.ExtWallet.OwnerGuid == ownerGuid).ToListAsync();

            return entities;
        }
    }
}
