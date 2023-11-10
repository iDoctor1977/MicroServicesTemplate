using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.WalletItem;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetWalletItemsFeature : IGetWalletItemsFeature
    {
        private readonly IDefaultMapper<WalletItemAppModel, WalletItemModel> _walletItemsMapper;
        private readonly IGetWalletItemsEfDepot _walletItemsEfDepot;
        private readonly ILogger<GetWalletItemsFeature> _logger;

        public GetWalletItemsFeature(
            IDefaultMapper<WalletItemAppModel, WalletItemModel> walletItemsMapper,
            IGetWalletItemsEfDepot walletItemsEfDepot,
            ILogger<GetWalletItemsFeature> logger)
        {
            _walletItemsEfDepot = walletItemsEfDepot;
            _walletItemsMapper = walletItemsMapper;
            _logger = logger;
        }

        public async Task<OperationResult<ICollection<WalletItemAppModel>>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Execute feature: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            ICollection<WalletItemModel> models;
            try
            {
                OperationResult<ICollection<WalletItemModel>> result = await _walletItemsEfDepot.ExecuteAsync(ownerGuid);
                models = result.Value;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);

                return new OperationResult<ICollection<WalletItemAppModel>>(OutcomeState.Failure, default, $"Data access failed: {e.Message}");
            }

            ICollection<WalletItemAppModel> appModels = new List<WalletItemAppModel>();
            if (models != null)
            {
                foreach (var walletItemModel in models)
                {
                    var appModel = _walletItemsMapper.Map(walletItemModel);

                    appModels.Add(appModel);
                }
            }
            
            return new OperationResult<ICollection<WalletItemAppModel>>(OutcomeState.Success, new List<WalletItemAppModel>(appModels));
        }
    }
}
