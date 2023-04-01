using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;

public class WalletAggregate
{
    public Guid Guid { get; private set; }
    public Guid OwnerGuid { get; private set; }
    public decimal TradingAllowedBalance { get; private set; }
    public decimal OperationAllowedBalance { get; private set; }
    public decimal Balance { get; private set; }
    public decimal Performance { get; private set; }
    public ICollection<WalletItemEntity> WalletItems { get; private set; }

    private readonly IDomainEntityFactory _domainEntityFactory;
    private readonly IDefaultMapper<WalletModel, WalletAggregate> _walletMapper;
    private readonly ILogger<WalletAggregate> _logger;

    #region Aggregate construction instance

    private WalletAggregate(
        IDomainEntityFactory domainEntityFactory,
        IDefaultMapper<WalletModel, WalletAggregate> walletMapper,
        ILogger<WalletAggregate> logger)
    {
        _walletMapper = walletMapper;
        _domainEntityFactory = domainEntityFactory;
        _logger = logger;

        WalletItems = new List<WalletItemEntity>();
    }

    /// <summary>
    /// For creation instance
    /// </summary>
    /// <param name="domainEntityFactory"></param>
    /// <param name="createWalletMapper"></param>
    /// <param name="logger"></param>
    /// <param name="model"></param>
    public WalletAggregate(
        IDomainEntityFactory domainEntityFactory,
        IDefaultMapper<CreateWalletModel, WalletAggregate> createWalletMapper,
        IDefaultMapper<WalletModel, WalletAggregate> walletMapper,
        ILogger<WalletAggregate> logger,
        CreateWalletModel model) : this(domainEntityFactory, walletMapper, logger)
    {
        SharedConstruction(model);

        createWalletMapper.Map(model, this);

        Guid = Guid.NewGuid();
    }

    /// <summary>
    /// For updating instance
    /// </summary>
    /// <param name="domainEntityFactory"></param>
    /// <param name="walletMapper"></param>
    /// <param name="logger"></param>
    /// <param name="model"></param>
    /// <exception cref="DomainValidationException{T}"></exception>
    public WalletAggregate(
        IDomainEntityFactory domainEntityFactory,
        IDefaultMapper<WalletModel, WalletAggregate> walletMapper,
        ILogger<WalletAggregate> logger,
        WalletModel model) : this(domainEntityFactory, walletMapper, logger)
    {
        if (model.Guid == Guid.Empty || model.Equals(null))
        {
            throw new DomainValidationException<WalletAggregate>("Guid is not valid");
        }
        if (model.Performance <= 0)
        {
            throw new DomainValidationException<WalletAggregate>("Performance is not valid");
        }

        SharedConstruction(model);

        try
        {
           WalletItems = model.WalletItems.Select(wa => _domainEntityFactory.GenerateAggregate<WalletItemModel, WalletItemEntity>(wa)).ToList();
        }
        catch (DomainValidationException<WalletItemEntity> e)
        {
            _logger.LogCritical($"{GetType().Name}: {e.Message}");

            throw new DomainValidationException<WalletAggregate>("Wallet item generation failed", e);
        }

        _walletMapper.Map(model, this);
    }

    private void SharedConstruction(WalletModelBase model)
    {
        if (model.Balance <= 0)
        {
            throw new DomainValidationException<WalletAggregate>("Balance is not valid");
        }
        if (model.OwnerGuid == Guid.Empty || model.Equals(null))
        {
            throw new DomainValidationException<WalletAggregate>("Owner guid is not valid");
        }
        if (model.TradingAllowedBalance <= 0)
        {
            throw new DomainValidationException<WalletAggregate>("Trading allowed balance is not valid");
        }
        if (model.OperationAllowedBalance <= 0)
        {
            throw new DomainValidationException<WalletAggregate>("Operation allowed balance is not valid");
        }
    }

    #endregion

    public decimal CalculateTradingAvailableBalance()
    {
        var investedValue = decimal.Zero;
        WalletItems.ToList().ForEach(walletItem => investedValue += walletItem.Amount );

        var tradingAvailable = TradingAllowedBalance - investedValue;

        if (TradingAllowedBalance < tradingAvailable || tradingAvailable < 0)
        {
            throw new DomainValidationException<WalletAggregate>("Trading allowed balance is not valid");
        }

        return tradingAvailable;
    }

    public WalletModel ToWalletModel()
    {
        var toModel = _walletMapper.Map(this);

        WalletItems.ToList().ForEach(wi => toModel.WalletItems.Add(wi.ToWalletItemModel()));

        return toModel;
    }
}