using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Mocks
{
    public class WalletEfRepositoryMock : EfRepository<Wallet>, IWalletRepository
    {
        protected WalletEfRepositoryMock(AppDbContext dbContext) : base(dbContext) { }

        public Task<IEnumerable<WalletItem>> ReadWalletItemsByOwnerGuidAsync(Guid ownerGuid)
        {
            throw new NotImplementedException();
        }

        public Task<Wallet?> ReadForOwnerGuidAsync(Guid ownerGuid)
        {
            throw new NotImplementedException();
        }
    }
}
