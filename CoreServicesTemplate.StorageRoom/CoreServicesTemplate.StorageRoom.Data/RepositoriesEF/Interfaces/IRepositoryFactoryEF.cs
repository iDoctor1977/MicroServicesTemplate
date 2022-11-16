using CoreServicesTemplate.Shared.Core.Interfaces.IRepository;

namespace CoreServicesTemplate.StorageRoom.Data.RepositoriesEF.Interfaces
{
    public interface IRepositoryFactoryEF
    {
        public T CreateRepository<T>(ProjectDbContext dbContext) where T : IRepository;
    }
}