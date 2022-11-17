using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;

namespace CoreServicesTemplate.StorageRoom.Data.RepositoriesEF.Interfaces
{
    public interface IRepositoryFactoryEF
    {
        public T CreateRepository<T>(ProjectDbContext dbContext) where T : IRepository;
    }
}