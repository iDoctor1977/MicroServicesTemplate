using System.Net.Http;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IServices
{
    public interface IStorageRoomService
    {
        Task<HttpResponseMessage> AddUserAsync(UserApiModel apiModel);
        HttpResponseMessage AddUser(UserApiModel apiModel);

        Task<UsersApiModel> GetUsersAsync();
        UsersApiModel GetUsers();
    }
}
