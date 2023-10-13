using System.Net.Http.Json;
using CoreServicesTemplate.Bus.Common.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Bus.Services
{
    public class BusService : IBusService
    {
        private readonly HttpClient _client;

        protected BusService()
        {
            _client = new HttpClient();
        }

        public async Task<OperationResult<ResponseEmailPropertiesApiDto>> GetEmailPropertiesAsync(Guid ownerGuid)
        {
            var url = ApiUrl.StorageRoomApi.GetEmailProperties();
            var apiModel = await _client.GetFromJsonAsync<ResponseEmailPropertiesApiDto>($"{url}/{ownerGuid}");

            if (apiModel != null)
            {
                return new OperationResult<ResponseEmailPropertiesApiDto>(apiModel);
            };

            return new OperationResult<ResponseEmailPropertiesApiDto>(OutcomeState.Failure, default," | Api return value is not valid.");
        }
    }
}