using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task AddCustomAsync(User entity);
        Task<IEnumerable<User>> GetAllCustomAsync();
        Task<User> GetByNameAsync(User entity);
        Task<User> GetByGuidAsync(User entity);
        Task<User> GetByIdAsync(User entity);
        Task UpdateCustomAsync(User entity);
        Task DeleteCustomAsync(User entity);
    }
}