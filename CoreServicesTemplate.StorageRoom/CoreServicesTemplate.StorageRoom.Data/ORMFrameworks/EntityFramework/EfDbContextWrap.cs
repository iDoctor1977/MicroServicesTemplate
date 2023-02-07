using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework
{
    public class EfDbContextWrap : IDbContextWrap
    {
        private readonly StorageRoomDbContext _dbContext;

        public EfDbContextWrap(StorageRoomDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}