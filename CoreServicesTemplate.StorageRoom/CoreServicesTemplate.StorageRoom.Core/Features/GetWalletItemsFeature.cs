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
        private readonly IDefaultMapper<WalletItemAppDto, WalletItemModel> _walletItemsMapper;
        private readonly IGetWalletItemsDepot _walletItemsDepot;
        private readonly ILogger<GetWalletItemsFeature> _logger;

        public GetWalletItemsFeature(
            IDefaultMapper<WalletItemAppDto, WalletItemModel> walletItemsMapper, 
            IGetWalletItemsDepot walletItemsDepot,
            ILogger<GetWalletItemsFeature> logger)
        {
            _walletItemsDepot = walletItemsDepot;
            _logger = logger;
            _walletItemsMapper = walletItemsMapper;
        }

        public async Task<OperationResult<ICollection<WalletItemAppDto>>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Get wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            ICollection<WalletItemModel>? walletItemsModel;
            try
            {
                var result = await _walletItemsDepot.ExecuteAsync(ownerGuid);
                walletItemsModel = result.Value;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult<ICollection<WalletItemAppDto>>(OutcomeState.Failure, default, $" | Data access failed: {e.Message}");
            }

            if (walletItemsModel != null)
            {
                var appModels = new List<WalletItemAppDto>(_walletItemsMapper.Map(walletItemsModel));
                return new OperationResult<ICollection<WalletItemAppDto>>(appModels);
            }

            return new OperationResult<ICollection<WalletItemAppDto>>(" | Data values is not valid.");
        }
    }
}
