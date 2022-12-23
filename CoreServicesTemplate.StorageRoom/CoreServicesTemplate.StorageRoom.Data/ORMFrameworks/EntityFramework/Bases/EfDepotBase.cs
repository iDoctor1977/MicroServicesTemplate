using System;
using System.Threading.Tasks;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases
{
    public class EfDepotBase : IDisposable
    {
        private readonly Lazy<StorageRoomDbContext> _dbContext;


        protected EfDepotBase(Lazy<StorageRoomDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        protected void Commit() => _dbContext.Value.SaveChanges();

        protected async Task CommitAsync() => await _dbContext.Value.SaveChangesAsync();

        public void Dispose() => _dbContext?.Value.Dispose();

        public async Task DisposeAsync() => await _dbContext.Value.DisposeAsync();
    }
}