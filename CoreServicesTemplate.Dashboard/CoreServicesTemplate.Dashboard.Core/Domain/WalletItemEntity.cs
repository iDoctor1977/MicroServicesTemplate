using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.WalletItems;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Exceptions;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Core.Domain;

public class WalletItemEntity : EntityBase<WalletItemModel, WalletItemEntity>
{
    public decimal Amount { get; private set; }
    public decimal BuyPrice { get; private set; }
    public DateTime BuyDate { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DateUpdated { get; private set; }
    public string ExtTicker { get; private set; }
    public Guid ExtMarketItemGuid { get; private set; }

    #region Domain child construction instance

    private WalletItemEntity(IDomainEntityFactory factoryBase,
    IDefaultMapper<WalletItemModel, WalletItemEntity> mapperBase,
    ILogger<WalletItemEntity> loggerBase) : base(factoryBase, mapperBase, loggerBase)
    {
    }

    /// <summary>
    /// To create instance
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="createMapper"></param>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="model"></param>
    public WalletItemEntity(
        IDomainEntityFactory factory,
        IDefaultMapper<CreateWalletItemModel, WalletItemEntity> createMapper,
        IDefaultMapper<WalletItemModel, WalletItemEntity> mapper,
        ILogger<WalletItemEntity> logger,
        CreateWalletItemModel model) : this(factory, mapper, logger)
    {
        SharedConstruction(model);

        createMapper.Map(model, this);
    }

    /// <summary>
    /// To update instance
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="mapper"></param>
    /// <param name="logger"></param>
    /// <param name="model"></param>
    /// <exception cref="DomainValidationException{WalletItemEntity}"></exception>
    public WalletItemEntity(
        IDomainEntityFactory factory,
        IDefaultMapper<WalletItemModel, WalletItemEntity> mapper,
        ILogger<WalletItemEntity> logger,
        WalletItemModel model) : this(factory, mapper, logger)
    {
        if (model.Amount <= 0)
        {
            throw new DomainValidationException<WalletItemEntity>($"{nameof(model.Amount)} is not valid");
        }
        if (model.DateUpdated.Equals(DateTime.MinValue))
        {
            throw new DomainValidationException<WalletItemEntity>($"{nameof(model.DateUpdated)} is not valid");
        }

        SharedConstruction(model);

        MapperBase.Map(model, this);
    }

    protected sealed override void SharedConstruction(IModelBase modelBase)
    {
        if (modelBase is WalletItemModelBase model)
        {
            if (model.BuyPrice <= 0)
            {
                throw new DomainValidationException<WalletItemEntity>($"{nameof(model.BuyPrice)} is not valid");
            }
            if (model.Quantity <= 0)
            {
                throw new DomainValidationException<WalletItemEntity>($"{nameof(model.Quantity)} is not valid");
            }
            if (model.BuyDate.Equals(DateTime.MinValue))
            {
                throw new DomainValidationException<WalletItemEntity>($"{nameof(model.BuyDate)} is not valid");
            }
        }
        else
        {
            throw new DomainValidationException<WalletItemEntity>($"{nameof(modelBase)} is not valid");
        }
    }

    #endregion

    public override WalletItemModel ToModel()
    {
        var toModel = MapperBase.Map(this);

        return toModel;
    }
}