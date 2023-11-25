using System.Net.Http.Json;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Services
{
    public class CreateWalletService : ICreateWalletService
    {
        private readonly IDefaultMapper<WalletModel, RequestStorageRoomCreateWalletApiDto> _mapper;
        private readonly ILogger<CreateWalletService> _logger;
        private readonly HttpClient _client;

        public CreateWalletService(IDefaultMapper<WalletModel, RequestStorageRoomCreateWalletApiDto> mapper, ILogger<CreateWalletService> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _client = new HttpClient();
        }

        public async Task<OperationResult<HttpResponseMessage>> ExecuteAsync(WalletModel model)
        {
            _logger.LogInformation("----- Execute service: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            //HTTP POST
            var requestApiDto = _mapper.Map(model);

            var url = ApiUrl.StorageRoomApi.CreateWallet();
            var responseMessage = await _client.PostAsJsonAsync($"{url}/{requestApiDto}", requestApiDto);

            return new OperationResult<HttpResponseMessage>(responseMessage);
        }
    }
}