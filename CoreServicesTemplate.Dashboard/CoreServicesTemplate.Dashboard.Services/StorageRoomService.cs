using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Services;

namespace CoreServicesTemplate.Dashboard.Services
{
    public class StorageRoomService : IStorageRoomService
    {
        private readonly HttpClient _client;

        public StorageRoomService()
        {
            _client = new HttpClient();
        }

        public async Task<HttpResponseMessage> AddUserAsync(UserApiModel apiModel)
        {
            //HTTP POST
            var url = API.StorageRoom.User.AddUserToStorageRoomUrl();
            var responseMessage = await _client.PostAsJsonAsync($"{url}/{apiModel}", apiModel);

            return responseMessage;
        }

        public HttpResponseMessage AddUser(UserApiModel apiModel)
        {
            throw new NotImplementedException();
        }

        public async Task<UsersApiModel?> GetUsersAsync()
        {
            //HTTP GET
            var url = API.StorageRoom.User.GetAllUserToStorageRoomUrl();
            var apiModel = await _client.GetFromJsonAsync<UsersApiModel>(url);

            return apiModel;
        }

        public UsersApiModel GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}