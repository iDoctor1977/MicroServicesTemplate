using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
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
    /// To create instance
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
    /// To update instance
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
            throw new DomainValidationException<WalletAggregate>($"{nameof(model.Guid)} is not valid");
        }
        if (model.Performance <= 0)
        {
            throw new DomainValidationException<WalletAggregate>($"{nameof(model.Performance)} is not valid");
        }

        SharedConstruction(model);

        try
        {
           WalletItems = model.WalletItems.Select(_domainEntityFactory.Generate<WalletItemModel, WalletItemEntity>).ToList();
        }
        catch (DomainValidationException<WalletItemEntity> e)
        {
            _logger.LogCritical($"{GetType().Name}: {e.Message}");

            throw new DomainValidationException<WalletAggregate>($"{GetType().Name}: wallet item generation failed", e);
        }

        _walletMapper.Map(model, this);
    }

    private void SharedConstruction(BaseWalletModel model)
    {
        if (model.Balance <= 0)
        {
            throw new DomainValidationException<WalletAggregate>($"{nameof(model.Balance)} is not valid");
        }
        if (model.OwnerGuid == Guid.Empty || model.Equals(null))
        {
            throw new DomainValidationException<WalletAggregate>($"{nameof(model.OwnerGuid)} is not valid");
        }
        if (model.TradingAllowedBalance <= 0)
        {
            throw new DomainValidationException<WalletAggregate>($"{nameof(model.TradingAllowedBalance)} is not valid");
        }
        if (model.OperationAllowedBalance <= 0)
        {
            throw new DomainValidationException<WalletAggregate>($"{nameof(model.OperationAllowedBalance)} is not valid");
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
            throw new DomainValidationException<WalletAggregate>($"{GetType().Name}: trading allowed balance is not valid");
        }

        return tradingAvailable;
    }

    public void AddWalletItems(ICollection<CreateWalletItemModel> walletItemModels)
    {
        try
        {
            foreach (var createWalletItemModel in walletItemModels)
            {
                var walletItemValue = (createWalletItemModel.BuyPrice * createWalletItemModel.Quantity);

                Balance -= walletItemValue;
                TradingAllowedBalance -= walletItemValue;

                var walletItemEntity = _domainEntityFactory.Generate<CreateWalletItemModel, WalletItemEntity>(createWalletItemModel);
                WalletItems.Add(walletItemEntity);
            }
        }
        catch (DomainValidationException<WalletItemEntity> e)
        {
            _logger.LogCritical($"{GetType().Name}: {e.Message}");

            throw new DomainValidationException<WalletAggregate>($"{GetType().Name}: wallet item generation failed", e);
        }
    }

    public WalletModel ToWalletModel()
    {
        var toModel = _walletMapper.Map(this);

        return toModel;
    }
}