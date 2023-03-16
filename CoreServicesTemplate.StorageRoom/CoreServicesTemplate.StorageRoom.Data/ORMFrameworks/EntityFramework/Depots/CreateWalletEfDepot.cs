using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;
using CoreServicesTemplate.StorageRoom.Data.Factories;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots;

public class CreateWalletEfDepot: UnitOfWorkDepotBase, ICreateWalletDepot
{
    private readonly IDefaultMapper<WalletModel, Entities.Wallet> _defaultWalletMapper;
    private readonly IWalletRepository _walletRepository;
    private readonly ILogger<CreateWalletEfDepot> _logger;

    public CreateWalletEfDepot(
        IAppDbContext dbContext,
        IRepositoryFactory repositoryFactory,
        IDefaultMapper<WalletModel, Entities.Wallet> defaultWalletMapper,
        ILogger<CreateWalletEfDepot> logger) : base(repositoryFactory, dbContext)
    {
        _defaultWalletMapper = defaultWalletMapper;
        _walletRepository = RepositoryFactory.GenerateCustomRepository<IWalletRepository>(DbContext);
        _logger = logger;
    }

    public async Task<OperationResult> ExecuteAsync(WalletModel appDto)
    {
        var walletEntity = _defaultWalletMapper.Map(appDto);

        await _walletRepository.AddAsync(walletEntity);

        await CommitAsync();

        return new OperationResult(OutcomeState.Success);
    }
}