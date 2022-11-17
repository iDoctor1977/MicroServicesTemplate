using System;
using System.Threading.Tasks;
using AutoMapper;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;

namespace CoreServicesTemplate.StorageRoom.Data.Bases
{
    public class DepotBaseEF : IDisposable
    {
        protected readonly IMapper Mapper;
        private readonly Lazy<DbContextProject> _dbContext;


        protected DepotBaseEF(IMapper mapper, Lazy<DbContextProject> dbContext)
        {
            Mapper = mapper;
            _dbContext = dbContext;
        }

        protected void Commit() => _dbContext.Value.SaveChanges();

        protected async Task CommitAsync() => await _dbContext.Value.SaveChangesAsync();

        public void Dispose() => _dbContext?.Value.Dispose();
    }
}