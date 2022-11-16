using System.Threading.Tasks;
using AutoMapper;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;

namespace CoreServicesTemplate.StorageRoom.Data.Bases
{
    public class DepotBase : IGenericDisposable
    {
        protected readonly IMapper Mapper;
        private readonly ProjectDbContext _dbContext;


        protected DepotBase(IMapper mapper, ProjectDbContext dbContext)
        {
            Mapper = mapper;
            _dbContext = dbContext;
        }

        public void Commit() => _dbContext.SaveChanges();

        public async Task CommitAsync() => await _dbContext.SaveChangesAsync();

        public void Dispose() => _dbContext.Dispose();

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
    }
}