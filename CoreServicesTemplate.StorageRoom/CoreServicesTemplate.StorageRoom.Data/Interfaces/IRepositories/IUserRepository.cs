using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<int> CreateEntity(UserApiModel model);
        Task<IEnumerable<UserApiModel>> ReadEntities();
    }
}