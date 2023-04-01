using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetWalletItemsFeature : IGetWalletItemsFeature
    {
        private readonly IDefaultMapper<ResponseWalletItemsAppDto, WalletItemModel> _walletItemsMapper;
        private readonly IGetWalletItemsEfDepot _walletItemsEfDepot;
        private readonly ILogger<GetWalletItemsFeature> _logger;

        public GetWalletItemsFeature(
            IDefaultMapper<ResponseWalletItemsAppDto, WalletItemModel> walletItemsMapper, 
            IGetWalletItemsEfDepot walletItemsEfDepot,
            ILogger<GetWalletItemsFeature> logger)
        {
            _walletItemsEfDepot = walletItemsEfDepot;
            _walletItemsMapper = walletItemsMapper;
            _logger = logger;
        }

        public async Task<OperationResult<ICollection<ResponseWalletItemsAppDto>>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Get wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            ICollection<WalletItemModel>? walletItemsModel;
            try
            {
                var result = await _walletItemsEfDepot.ExecuteAsync(ownerGuid);
                walletItemsModel = result.Value;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult<ICollection<ResponseWalletItemsAppDto>>(OutcomeState.Failure, default, $" | Data access failed: {e.Message}");
            }

            if (walletItemsModel != null)
            {
                var appModels = new List<ResponseWalletItemsAppDto>(_walletItemsMapper.Map(walletItemsModel));
                return new OperationResult<ICollection<ResponseWalletItemsAppDto>>(appModels);
            }

            return new OperationResult<ICollection<ResponseWalletItemsAppDto>>(" | Data values is not valid.");
        }
    }
}
