using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task AddEntity(User entity);
        Task<IEnumerable<User>> GetEntities();
        Task UpdateEntity(User entity);
        Task<User> GetEntityByName(User entity);
        Task<User> GetEntityByGuid(User entity);
        Task<User> GetEntityById(User entity);
        Task DeleteEntity(User entity);
    }
}