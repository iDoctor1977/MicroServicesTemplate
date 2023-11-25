using System.Net.Http.Json;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Services
{
    public class GetWalletService : IGetWalletService
    {
        private readonly IDefaultMapper<WalletModel, ResponseStorageRoomGetWalletApiDto> _mapper;
        private readonly ILogger<GetWalletService> _logger;
        private readonly HttpClient _client;

        public GetWalletService(IDefaultMapper<WalletModel, ResponseStorageRoomGetWalletApiDto> mapper, ILogger<GetWalletService> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _client = new HttpClient();
        }

        public async Task<OperationResult<WalletModel>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Execute service: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            //HTTP GET
            var url = ApiUrl.StorageRoomApi.GetWallet();
            var responseApiModel = await _client.GetFromJsonAsync<ResponseStorageRoomGetWalletApiDto>($"{url}/{ownerGuid}");

            if (responseApiModel != null)
            {
                var model = _mapper.Map(responseApiModel);

                return new OperationResult<WalletModel>(model);
            };

            return new OperationResult<WalletModel>(OutcomeState.Failure, default, " | Api return value is not valid.");
        }
    }
}