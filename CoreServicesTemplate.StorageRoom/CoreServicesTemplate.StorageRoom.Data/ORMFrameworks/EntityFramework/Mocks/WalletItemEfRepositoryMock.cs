using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Mocks
{
    public class WalletItemEfRepositoryMock : EfRepository<WalletItem>, IWalletItemRepository
    {
        protected WalletItemEfRepositoryMock(AppDbContext dbContext) : base(dbContext) { }

        public Task<IEnumerable<WalletItem>> ReadWalletItemsByOwnerGuidAsync(Guid ownerGuid)
        {
            throw new NotImplementedException();
        }
    }
}
