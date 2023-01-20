using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<OperationStatusResult> AddCustomAsync(User entity);
        Task<IEnumerable<User>> GetAllCustomAsync();
        Task<User> GetByNameAsync(User entity);
        Task<User> GetByGuidAsync(User entity);
        Task<User> GetByIdAsync(User entity);
        Task<OperationStatusResult> UpdateCustomAsync(User entity);
        Task<OperationStatusResult> DeleteCustomAsync(User entity);
    }
}