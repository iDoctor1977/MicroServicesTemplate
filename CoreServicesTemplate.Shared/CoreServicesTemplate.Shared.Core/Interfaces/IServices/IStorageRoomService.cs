using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IServices
{
    public interface IStorageRoomService
    {
        Task<HttpResponseMessage> CreateUserAsync(UserApiModel model);
        Task<IEnumerable<UserApiModel>> ReadUsersAsync();
    }
}
