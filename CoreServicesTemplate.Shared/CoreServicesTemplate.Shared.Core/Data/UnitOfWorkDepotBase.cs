using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IData;

namespace CoreServicesTemplate.Shared.Core.Data
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