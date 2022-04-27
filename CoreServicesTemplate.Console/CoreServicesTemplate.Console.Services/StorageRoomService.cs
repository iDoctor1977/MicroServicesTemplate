using System.Collections.Generic;
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

        public async Task<HttpResponseMessage> CreateUserAsync(UserApiModel model)
        {
            //HTTP POST
            var responseMessage = await _client.PostAsJsonAsync($"{ApiUrlStrings.StorageRoomUserControllerLocalhostUrl}", model);

            return responseMessage;
        }

        public async Task<IEnumerable<UserApiModel>> ReadUsersAsync()
        {
            //HTTP GET
            var users = await _client.GetFromJsonAsync<IEnumerable<UserApiModel>>($"{ApiUrlStrings.StorageRoomUserControllerLocalhostUrl}");

            return users;
        }
    }
}