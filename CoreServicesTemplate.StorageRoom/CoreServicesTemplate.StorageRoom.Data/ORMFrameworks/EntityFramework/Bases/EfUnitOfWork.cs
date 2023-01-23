using System;
using System.Threading.Tasks;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases
{
    public class EfUnitOfWork : IDisposable
    {
        private readonly StorageRoomDbContext _dbContext;


        protected EfUnitOfWork(StorageRoomDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit() => _dbContext.SaveChanges();

        public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

        public void Dispose() => _dbContext?.Dispose();

        public async Task DisposeAsync() => await _dbContext.DisposeAsync();
    }
}