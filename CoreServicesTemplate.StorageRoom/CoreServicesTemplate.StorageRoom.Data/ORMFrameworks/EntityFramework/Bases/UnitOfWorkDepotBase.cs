using CoreServicesTemplate.StorageRoom.Data.Factories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases
{
    public class UnitOfWorkDepotBase : IAppDbContext
    {
        protected IAppDbContext DbContext { get; }
        protected IRepositoryFactory RepositoryFactory { get; }

        protected UnitOfWorkDepotBase(IRepositoryFactory repositoryFactory, IAppDbContext dbContext)
        {
            RepositoryFactory = repositoryFactory;
            DbContext = dbContext;
        }

        public void Commit()
        {
            DbContext.Commit();
        }

        public async Task CommitAsync()
        {
            await DbContext.CommitAsync();
        }
    }
}