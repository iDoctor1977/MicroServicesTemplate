using System;
using System.Threading.Tasks;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases
{
    public class EfUnitOfWork : IDisposable
    {
        private readonly Lazy<StorageRoomDbContext> _dbContext;


        protected EfUnitOfWork(Lazy<StorageRoomDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit() => _dbContext.Value.SaveChanges();

        public async Task CommitAsync() => await _dbContext.Value.SaveChangesAsync();

        public void Dispose() => _dbContext?.Value.Dispose();

        public async Task DisposeAsync() => await _dbContext.Value.DisposeAsync();
    }
}