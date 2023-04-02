using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Dtos.Wallet;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class CreateWalletFeature : ICreateWalletFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IDefaultMapper<CreateWalletAppModel, CreateWalletApiDto> _walletMapper;
        private readonly ILogger<CreateWalletFeature> _logger;

        public CreateWalletFeature(
            IStorageRoomService storageRoomService, 
            IDefaultMapper<CreateWalletAppModel, CreateWalletApiDto> walletMapper,
            ILogger<CreateWalletFeature> logger) 
        {
            _storageRoomService = storageRoomService;
            _walletMapper = walletMapper;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteAsync(CreateWalletAppModel model)
        {
            _logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var apiModel = _walletMapper.Map(model);

            var responseMessage = await _storageRoomService.CreateNewWalletAsync(apiModel);

            return new OperationResult(OutcomeState.Success, responseMessage);
        }
    }
}
