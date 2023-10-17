using System.Net.Http.Json;
using CoreServicesTemplate.Bus.Common.Interfaces.IServices;
using CoreServicesTemplate.Bus.Common.Models;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Bus.Services
{
    public class BusService : IBusService
    {
        private readonly IDefaultMapper<ResponseEmailPropertiesApiDto, EmailPropertiesModel> _mapper;
        private readonly HttpClient _client;

        public BusService(IDefaultMapper<ResponseEmailPropertiesApiDto, EmailPropertiesModel> mapper)
        {
            _mapper = mapper;
            _client = new HttpClient();
        }

        public async Task<OperationResult<EmailPropertiesModel>> GetEmailPropertiesAsync(Guid ownerGuid)
        {
            var url = ApiUrl.StorageRoomApi.GetEmailProperties();
            var apiModel = await _client.GetFromJsonAsync<ResponseEmailPropertiesApiDto>($"{url}/{ownerGuid}");

            if (apiModel != null)
            {
                var model = _mapper.Map(apiModel);

                return new OperationResult<EmailPropertiesModel>(model);
            };

            return new OperationResult<EmailPropertiesModel>(OutcomeState.Failure, default," | Api return value is not valid.");
        }
    }
}