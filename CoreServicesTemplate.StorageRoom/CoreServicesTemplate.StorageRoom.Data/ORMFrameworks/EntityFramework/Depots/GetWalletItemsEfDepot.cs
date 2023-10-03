using CoreServicesTemplate.Shared.Core.Data;
using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetWalletItemsEfDepot : UnitOfWorkDepotBase, IGetWalletItemsEfDepot
    {
        private readonly IDefaultMapper<WalletItemModel, WalletItem> _walletItemMapper;
        private readonly IWalletItemRepository _walletItemRepository;
        private readonly ILogger<GetWalletItemsEfDepot> _logger;

        public GetWalletItemsEfDepot(
            IUnitOfWorkContext dbContext,
            IRepositoryFactory repositoryFactory,
            IDefaultMapper<WalletItemModel, WalletItem> walletItemMapper,
            ILogger<GetWalletItemsEfDepot> logger) : base(repositoryFactory, dbContext)
        {
            _walletItemMapper = walletItemMapper;
            _walletItemRepository = RepositoryFactory.GenerateCustomRepository<IWalletItemRepository>(dbContext);
            _logger = logger;
        }

        public async Task<OperationResult<ICollection<WalletItemModel>>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Get wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var entities = await _walletItemRepository.ReadWalletItemsByOwnerGuidAsync(ownerGuid);

            var aggModels = new List<WalletItemModel>(_walletItemMapper.Map(entities.ToList()));
            return new OperationResult<ICollection<WalletItemModel>>(aggModels);
        }
    }
}
