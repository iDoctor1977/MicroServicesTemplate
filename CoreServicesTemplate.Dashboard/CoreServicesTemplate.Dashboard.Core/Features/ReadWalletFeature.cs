using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models.AppModels.Wallets;
using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class ReadWalletFeature : IGetWalletFeature
    {
        private readonly IGetWalletService _getWalletService;
        private readonly IDefaultMapper<WalletAppModel, WalletModel> _mapper;
        private readonly ILogger<ReadWalletFeature> _logger;

        public ReadWalletFeature(
            IGetWalletService getWalletService, 
            IDefaultMapper<WalletAppModel, WalletModel> mapper,
            ILogger<ReadWalletFeature> logger)
        {
            _getWalletService = getWalletService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult<WalletAppModel>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Read wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var result = await _getWalletService.ExecuteAsync(ownerGuid);

            if (result.Value != null)
            {
                var appModel = _mapper.Map(result.Value);

                return new OperationResult<WalletAppModel>(appModel);
            }

            return new OperationResult<WalletAppModel>("Wallet value is not valid.", result);
        }
    }
}