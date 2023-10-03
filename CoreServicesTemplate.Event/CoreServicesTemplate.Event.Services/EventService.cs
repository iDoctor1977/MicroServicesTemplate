using CoreServicesTemplate.Event.Common.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.DtoModels.Wallet;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Results;
using System.Net.Http.Json;

namespace CoreServicesTemplate.Event.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _client;

        public EventService()
        {
            _client = new HttpClient();
        }

        public async Task<OperationResult<WalletApiDto>> GetWalletAsync(Guid ownerGuid)
        {
            var url = ApiUrl.StorageRoomApi.GetWallet();
            var apiModel = await _client.GetFromJsonAsync<WalletApiDto>($"{url}/{ownerGuid}");

            if (apiModel != null)
            {
                return new OperationResult<WalletApiDto>(apiModel);
            };

            return new OperationResult<WalletApiDto>("Api return value is not valid.");
        }
    }
}