using System.Net.Http.Json;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Services
{
    public class StorageRoomService : IStorageRoomService
    {
        private readonly IDefaultMapper<CreateWalletModel, RequestCreateWalletApiDto> _createWalletMapper;
        private readonly IDefaultMapper<WalletModel, ResponseWalletApiDto> _walletMapper;
        private readonly ILogger<StorageRoomService> _logger;
        private readonly HttpClient _client;

        public StorageRoomService(
            IDefaultMapper<CreateWalletModel, RequestCreateWalletApiDto> createWalletMapper,
            ILogger<StorageRoomService> logger, IDefaultMapper<WalletModel, ResponseWalletApiDto> walletMapper)
        {
            _logger = logger;
            _walletMapper = walletMapper;
            _createWalletMapper = createWalletMapper;
            _client = new HttpClient();
        }

        public async Task<OperationResult<HttpResponseMessage>> CreateNewWalletAsync(CreateWalletModel model)
        {
            _logger.LogInformation("----- Execute service: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            //HTTP POST
            var apiDto = _createWalletMapper.Map(model);

            var url = ApiUrl.StorageRoomApi.CreateWallet();
            var responseMessage = await _client.PostAsJsonAsync($"{url}/{apiDto}", apiDto);

            return new OperationResult<HttpResponseMessage>(responseMessage);
        }

        public async Task<OperationResult<WalletModel>> GetWalletAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Execute service: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            //HTTP GET
            var url = ApiUrl.StorageRoomApi.GetWallet();
            var apiModel = await _client.GetFromJsonAsync<ResponseWalletApiDto>($"{url}/{ownerGuid}");

            if (apiModel != null)
            {
                var model = _walletMapper.Map(apiModel);

                return new OperationResult<WalletModel>(model);
            };

            return new OperationResult<WalletModel>(OutcomeState.Failure, default, " | Api return value is not valid.");
        }
    }
}