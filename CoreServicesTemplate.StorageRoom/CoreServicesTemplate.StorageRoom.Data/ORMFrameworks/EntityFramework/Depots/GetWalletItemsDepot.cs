using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Factories;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetWalletItemsDepot : UnitOfWorkDepotBase, IGetWalletItemsDepot
    {
        private readonly IDefaultMapper<WalletItemModel, WalletItem> _walletItemMapper;
        private readonly IWalletItemRepository _walletItemRepository;
        private readonly ILogger<GetWalletItemsDepot> _logger;

        public GetWalletItemsDepot(
            IAppDbContext dbContext,
            IRepositoryFactory repositoryFactory,
            IDefaultMapper<WalletItemModel, WalletItem> walletItemMapper,
            ILogger<GetWalletItemsDepot> logger) : base(repositoryFactory, dbContext)
        {
            _walletItemMapper = walletItemMapper;
            _walletItemRepository = RepositoryFactory.GenerateCustomRepository<IWalletItemRepository>(dbContext);
            _logger = logger;
        }

        public async Task<OperationResult<ICollection<WalletItemModel>>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Get WalletItems: {@Class} {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var walletEntities = await _walletItemRepository.ReadWalletItemsByOwnerGuidAsync(ownerGuid);

            ICollection<WalletItemModel> walletItemModels = new List<WalletItemModel>();
            foreach (var walletEntity in walletEntities)
            {
                var walletModel = _walletItemMapper.Map(walletEntity);

                walletItemModels.Add(walletModel);
            }

            return new OperationResult<ICollection<WalletItemModel>>(OutcomeState.Success, walletItemModels);
        }
    }
}
