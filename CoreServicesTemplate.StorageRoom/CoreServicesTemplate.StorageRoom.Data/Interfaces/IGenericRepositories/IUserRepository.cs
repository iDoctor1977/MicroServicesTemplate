using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IRepository;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Interfaces.IGenericRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task CreateEntity(User entity);
        Task<IEnumerable<User>> ReadEntities();
        Task UpdateEntity(User entity);
        Task<User> ReadEntityByName(User entity);
        Task<User> ReadEntityByGuid(User entity);
        Task DeleteEntity(User entity);
    }
}