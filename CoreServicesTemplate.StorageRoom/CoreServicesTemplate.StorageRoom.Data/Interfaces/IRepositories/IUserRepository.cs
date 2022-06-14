using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<int> CreateEntity(User entity);
        Task<IEnumerable<User>> ReadEntities();
    }
}