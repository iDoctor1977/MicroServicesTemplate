using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class ReadWalletFeature : IGetWalletFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IDefaultMapper<WalletAppModel, WalletModel> _walletMapper;
        private readonly ILogger<CreateWalletFeature> _logger;

        public ReadWalletFeature(
            IStorageRoomService storageRoomService, 
            IDefaultMapper<WalletAppModel, WalletModel> walletMapper,
            ILogger<CreateWalletFeature> logger)
        {
            _storageRoomService = storageRoomService;
            _walletMapper = walletMapper;
            _logger = logger;
        }

        public async Task<OperationResult<WalletAppModel>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Read wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var result = await _storageRoomService.GetWalletAsync(ownerGuid);

            if (result.Value != null)
            {
                var appModel = _walletMapper.Map(result.Value);

                return new OperationResult<WalletAppModel>(appModel);
            }

            return new OperationResult<WalletAppModel>("Wallet value is not valid.", result);
        }
    }
}