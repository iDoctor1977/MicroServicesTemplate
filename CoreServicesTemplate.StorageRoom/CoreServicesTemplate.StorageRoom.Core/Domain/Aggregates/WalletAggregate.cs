using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Exceptions;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.WalletItem;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;

public class WalletAggregate : EntityBase<WalletModel, WalletAggregate>
{
    public Guid Guid { get; private set; }
    public Guid OwnerGuid { get; private set; }
    public decimal TradingAllowedBalance { get; private set; }
    public decimal OperationAllowedBalance { get; private set; }
    public decimal Balance { get; private set; }
    public decimal Performance { get; private set; }
    public ICollection<WalletItemEntity> WalletItems { get; private set; }

    #region Domain aggregate construction instance

    private WalletAggregate(
        IDomainEntityFactory factoryBase,
        IDefaultMapper<WalletModel, WalletAggregate> mapperBase,
        ILogger<WalletAggregate> loggerBase) : base(factoryBase, mapperBase, loggerBase)
    {
        WalletItems = new List<WalletItemEntity>();
    }

    /// <summary>
    /// To create instance
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="CreateMapper"></param>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="model"></param>
    public WalletAggregate(
        IDomainEntityFactory factory,
        IDefaultMapper<CreateWalletModel, WalletAggregate> createMapper,
        IDefaultMapper<WalletModel, WalletAggregate> mapper,
        ILogger<WalletAggregate> logger,
        CreateWalletModel model) : this(factory, mapper, logger)
    {
        SharedConstruction(model);

        createMapper.Map(model, this);

        Guid = Guid.NewGuid();
    }

    /// <summary>
    /// To update instance
    /// </summary>
    /// <param name="factoryBase"></param>
    /// <param name="mapperBase"></param>
    /// <param name="loggerBase"></param>
    /// <param name="model"></param>
    /// <exception cref="DomainValidationException{T}"></exception>
    public WalletAggregate(
        IDomainEntityFactory factoryBase,
        IDefaultMapper<WalletModel, WalletAggregate> mapperBase,
        ILogger<WalletAggregate> loggerBase,
        WalletModel model) : this(factoryBase, mapperBase, loggerBase)
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
           WalletItems = model.WalletItems.Select(FactoryBase.Generate<WalletItemModel, WalletItemEntity>).ToList();
        }
        catch (DomainValidationException<WalletItemEntity> e)
        {
            LoggerBase.LogCritical($"{GetType().Name}: {e.Message}");

            throw new DomainValidationException<WalletAggregate>($"{GetType().Name}: wallet item generation failed", e);
        }

        MapperBase.Map(model, this);
    }

    protected override void SharedConstruction(IModelBase modelBase)
    {
        if (modelBase is WalletModelBase model)
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
        else
        {
            throw new DomainValidationException<WalletAggregate>($"{nameof(model)} is not valid");
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

                var walletItemEntity = FactoryBase.Generate<CreateWalletItemModel, WalletItemEntity>(createWalletItemModel);
                WalletItems.Add(walletItemEntity);
            }
        }
        catch (DomainValidationException<WalletItemEntity> e)
        {
            LoggerBase.LogCritical($"{GetType().Name}: {e.Message}");

            throw new DomainValidationException<WalletAggregate>($"{GetType().Name}: wallet item generation failed", e);
        }
    }

    public override WalletModel ToModel()
    {
        var toModel = MapperBase.Map(this);

        return toModel;
    }
}