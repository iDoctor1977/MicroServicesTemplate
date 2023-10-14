using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;

namespace CoreServicesTemplate.Shared.Core.Bases
{
    public class UnitOfWorkDepotBase : IUnitOfWorkContext
    {
        protected IUnitOfWorkContext DbContext { get; }
        protected IRepositoryFactory RepositoryFactory { get; }

        protected UnitOfWorkDepotBase(IRepositoryFactory repositoryFactory, IUnitOfWorkContext dbContext)
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