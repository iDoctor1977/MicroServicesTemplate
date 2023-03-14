using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases
{
    public class UnitOfWorkDepotBase : IUnitOfWorkContext
    {
        private readonly IDbContextWrap _dbContextWrap;


        protected UnitOfWorkDepotBase(IDbContextWrap dbContextWrap)
        {
            _dbContextWrap = dbContextWrap;
        }

        public void Commit() => _dbContextWrap.SaveChanges();

        public async Task CommitAsync() => await _dbContextWrap.SaveChangesAsync();
    }
}