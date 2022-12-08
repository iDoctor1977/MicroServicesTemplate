using System;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;

namespace CoreServicesTemplate.StorageRoom.Data.Bases
{
    public class DepotBaseEF : IDisposable
    {
        private readonly Lazy<DbContextProject> _dbContext;


        protected DepotBaseEF(Lazy<DbContextProject> dbContext)
        {
            _dbContext = dbContext;
        }

        protected void Commit() => _dbContext.Value.SaveChanges();

        protected async Task CommitAsync() => await _dbContext.Value.SaveChangesAsync();

        public void Dispose() => _dbContext?.Value.Dispose();

        public async Task DisposeAsync() => await _dbContext.Value.DisposeAsync();
    }
}