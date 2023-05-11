using CoreServicesTemplate.Shared.Core.Data;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots;

public class GetTradingAvailableBalanceEfDepot : UnitOfWorkDepotBase, IGetTradingAvailableBalanceDepot
{
    private readonly ICustomMapper<WalletModel, Entities.Wallet> _customWalletMapper;
    private readonly IWalletRepository _walletRepository;
    private readonly ILogger<GetTradingAvailableBalanceEfDepot> _logger;

    public GetTradingAvailableBalanceEfDepot(
        IAppDbContext dbContext,
        ICustomMapper<WalletModel, Entities.Wallet> customWalletMapper,
        ILogger<GetTradingAvailableBalanceEfDepot> logger,
        IRepositoryFactory repositoryFactory) : base(repositoryFactory, dbContext)
    {
        _customWalletMapper = customWalletMapper;
        _walletRepository = repositoryFactory.GenerateCustomRepository<IWalletRepository>(DbContext);
        _logger = logger;
    }

    public async Task<OperationResult<WalletModel>> ExecuteAsync(Guid ownerGuid)
    {
        _logger.LogInformation("----- Get trading available balance: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        var walletEntity = await _walletRepository.ReadForOwnerGuidAsync(ownerGuid);

        if (walletEntity != null)
        {
            var walletModel = _customWalletMapper.Map(walletEntity);

            return new OperationResult<WalletModel>(OutcomeState.Success, walletModel);
        }

        return new OperationResult<WalletModel>(" | Data values is tot valid.");
    }
}