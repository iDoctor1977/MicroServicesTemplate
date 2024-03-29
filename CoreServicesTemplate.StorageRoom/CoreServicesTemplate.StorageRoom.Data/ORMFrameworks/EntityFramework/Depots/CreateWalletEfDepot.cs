using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots;

public class CreateWalletEfDepot: UnitOfWorkDepotBase, ICreateWalletDepot
{
    private readonly IDefaultMapper<WalletModel, Entities.Wallet> _walletMapper;
    private readonly IWalletRepository _walletRepository;
    private readonly ILogger<CreateWalletEfDepot> _logger;

    public CreateWalletEfDepot(
        IUnitOfWorkContext dbContext,
        IRepositoryFactory repositoryFactory,
        IDefaultMapper<WalletModel, Entities.Wallet> defaultWalletMapper,
        ILogger<CreateWalletEfDepot> logger) : base(repositoryFactory, dbContext)
    {
        _walletMapper = defaultWalletMapper;
        _walletRepository = RepositoryFactory.GenerateCustomRepository<IWalletRepository>(DbContext);
        _logger = logger;
    }

    public async Task<OperationResult> ExecuteAsync(WalletModel model)
    {
        _logger.LogInformation("----- Execute depot: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        var entity = _walletMapper.Map(model);

        await _walletRepository.AddAsync(entity);

        await CommitAsync();

        return new OperationResult(OutcomeState.Success);
    }
}