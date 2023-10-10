using System.Net.Http.Json;
using CoreServicesTemplate.Bus.Common.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Bus.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _client;

        public EventService()
        {
            _client = new HttpClient();
        }

        public async Task<OperationResult<CreateWalletEventApiDto?>> CreateWalletEventAsync(Guid ownerGuid)
        {
            var url = ApiUrl.StorageRoomApi.CreateWalletEvent();
            var apiModel = await _client.GetFromJsonAsync<CreateWalletEventApiDto>($"{url}/{ownerGuid}");

            if (apiModel != null)
            {
                return new OperationResult<CreateWalletEventApiDto?>(apiModel);
            };

            return new OperationResult<CreateWalletEventApiDto?>(OutcomeState.Failure, default," | Api return value is not valid.");
        }
    }
}