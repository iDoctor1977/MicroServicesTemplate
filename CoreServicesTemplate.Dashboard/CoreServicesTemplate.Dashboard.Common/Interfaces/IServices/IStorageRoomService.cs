using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Common.Interfaces.IServices
{
    public interface IStorageRoomService
    {
        Task<HttpResponseMessage> AddUserAsync(UserApiModel apiModel);
        HttpResponseMessage AddUser(UserApiModel apiModel);

        Task<UsersApiModel> GetUsersAsync();
        UsersApiModel GetUsers();
    }
}
