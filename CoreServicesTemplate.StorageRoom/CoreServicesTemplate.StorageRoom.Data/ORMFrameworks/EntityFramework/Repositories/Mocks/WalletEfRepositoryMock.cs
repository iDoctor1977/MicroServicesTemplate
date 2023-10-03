using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories.Mocks
{
    public class WalletEfRepositoryMock : EfRepository<Wallet>, IWalletRepository
    {
        protected WalletEfRepositoryMock(UnitOfWorkContext dbContext) : base(dbContext) { }

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
