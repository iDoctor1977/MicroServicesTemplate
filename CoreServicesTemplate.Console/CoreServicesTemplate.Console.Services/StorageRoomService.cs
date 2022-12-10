using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.Shared.Core.Resources;

namespace CoreServicesTemplate.Console.Services
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
            var responseMessage = await _client.PostAsJsonAsync($"{ApiUrlStrings.StorageRoomUserControllerLocalhostAddUserUrl}/{apiModel}", apiModel);

            return responseMessage;
        }

        public async Task<UsersApiModel> GetUsersAsync()
        {
            //HTTP GET
            var apiModel = await _client.GetFromJsonAsync<UsersApiModel>($"{ApiUrlStrings.StorageRoomUserControllerLocalhostGetUsersUrl}");

            return apiModel;
        }
    }
}