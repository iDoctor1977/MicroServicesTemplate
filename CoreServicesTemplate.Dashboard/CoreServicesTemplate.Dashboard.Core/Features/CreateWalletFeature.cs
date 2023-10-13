using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class CreateWalletFeature : ICreateWalletFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IDefaultMapper<CreateWalletAppModel, CreateWalletModel> _walletMapper;
        private readonly ILogger<CreateWalletFeature> _logger;

        public CreateWalletFeature(
            IStorageRoomService storageRoomService,
            IDefaultMapper<CreateWalletAppModel, CreateWalletModel> walletMapper,
            ILogger<CreateWalletFeature> logger) 
        {
            _storageRoomService = storageRoomService;
            _walletMapper = walletMapper;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteAsync(CreateWalletAppModel appModel)
        {
            _logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var model = _walletMapper.Map(appModel);

            var responseMessage = await _storageRoomService.CreateNewWalletAsync(model);

            return new OperationResult(OutcomeState.Success, responseMessage);
        }
    }
}
