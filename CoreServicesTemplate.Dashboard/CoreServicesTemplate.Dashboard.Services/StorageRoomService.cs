using System.Net.Http.Json;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Dtos.Wallet;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Dashboard.Services
{
    public class StorageRoomService : IStorageRoomService
    {
        private readonly HttpClient _client;

        public StorageRoomService()
        {
            _client = new HttpClient();
        }

        public async Task<OperationResult<HttpResponseMessage>> PostWalletAsync(CreateWalletApiDto apiModel)
        {
            //HTTP POST
            var url = ApiUrl.StorageRoom.Wallet.CreateWalletToStorageRoom();
            var responseMessage = await _client.PostAsJsonAsync($"{url}/{apiModel}", apiModel);

            return new OperationResult<HttpResponseMessage>(responseMessage);
        }

        public async Task<OperationResult<WalletApiDto>> GetWalletAsync(Guid ownerGuid)
        {
            //HTTP GET
            var url = ApiUrl.StorageRoom.Wallet.GetWalletToStorageRoom();
            var apiModel = await _client.GetFromJsonAsync<WalletApiDto>($"{url}/{ownerGuid}");

            if (apiModel != null)
            {
                return new OperationResult<WalletApiDto>(apiModel);
            };

            return new OperationResult<WalletApiDto>("Api return value is not valid.");
        }
    }
}