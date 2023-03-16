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
            ICollection<WalletItemModel> walletItemsModel;
            try
            {
                var result = await _walletItemsDepot.ExecuteAsync(ownerGuid);
                walletItemsModel = result.Value;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult<ICollection<WalletItemAppDto>>(OutcomeState.Failure, default, $"Data access failed: {e.Message}");
            }

            ICollection<WalletItemAppDto> walletItems = new List<WalletItemAppDto>();
            foreach (var walletItemModel in walletItemsModel)
            {
                var walletItemApp = _walletItemsMapper.Map(walletItemModel);

                walletItems.Add(walletItemApp);
            }

            return new OperationResult<ICollection<WalletItemAppDto>>(OutcomeState.Success, new List<WalletItemAppDto>(walletItems));
        }
    }
}
