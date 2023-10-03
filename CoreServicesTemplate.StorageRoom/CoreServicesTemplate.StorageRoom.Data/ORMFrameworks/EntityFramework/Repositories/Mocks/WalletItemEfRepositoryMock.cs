using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Repositories.Mocks
{
    public class WalletItemEfRepositoryMock : EfRepository<WalletItem>, IWalletItemRepository
    {
        protected WalletItemEfRepositoryMock(AppEfContext dbContext) : base(dbContext) { }

        public Task<IEnumerable<WalletItem>> ReadWalletItemsByOwnerGuidAsync(Guid ownerGuid)
        {
            throw new NotImplementedException();
        }
    }
}
