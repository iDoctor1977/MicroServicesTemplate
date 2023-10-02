using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.WalletItem;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetWalletItemsFeature : IGetWalletItemsFeature
    {
        private readonly IDefaultMapper<WalletItemAppDto, WalletItemModel> _walletItemsMapper;
        private readonly IGetWalletItemsEfDepot _walletItemsEfDepot;
        private readonly ILogger<GetWalletItemsFeature> _logger;

        public GetWalletItemsFeature(
            IDefaultMapper<WalletItemAppDto, WalletItemModel> walletItemsMapper,
            IGetWalletItemsEfDepot walletItemsEfDepot,
            ILogger<GetWalletItemsFeature> logger)
        {
            _walletItemsEfDepot = walletItemsEfDepot;
            _walletItemsMapper = walletItemsMapper;
            _logger = logger;
        }

        public async Task<OperationResult<ICollection<WalletItemAppDto>>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("Execute feature: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            ICollection<WalletItemModel>? walletItemsModel;
            try
            {
                OperationResult<ICollection<WalletItemModel>> result = await _walletItemsEfDepot.ExecuteAsync(ownerGuid);
                walletItemsModel = result.Value;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);

                return new OperationResult<ICollection<WalletItemAppDto>>(OutcomeState.Failure, default, $"Data access failed: {e.Message}");
            }

            ICollection<WalletItemAppDto> walletItems = new List<WalletItemAppDto>();
            if (walletItemsModel != null)
            {
                foreach (var walletItemModel in walletItemsModel)
                {
                    var walletItemApp = _walletItemsMapper.Map(walletItemModel);

                    walletItems.Add(walletItemApp);
                }
            }
            
            return new OperationResult<ICollection<WalletItemAppDto>>(OutcomeState.Success, new List<WalletItemAppDto>(walletItems));
        }
    }
}
