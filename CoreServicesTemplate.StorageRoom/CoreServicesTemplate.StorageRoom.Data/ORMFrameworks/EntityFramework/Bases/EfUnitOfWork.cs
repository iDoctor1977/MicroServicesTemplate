using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases
{
    public class EfUnitOfWork : IDisposable
    {
        private readonly IDbContextWrap _dbContextWrap;


        protected EfUnitOfWork(IDbContextWrap dbContextWrap)
        {
            _dbContextWrap = dbContextWrap;
        }

        protected void Commit() => _dbContextWrap.SaveChanges();

        protected async Task CommitAsync() => await _dbContextWrap.SaveChangesAsync();

        public void Dispose() => _dbContextWrap.Dispose();

        public async Task DisposeAsync() => await _dbContextWrap.DisposeAsync();
    }
}