using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.WalletItems;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Exceptions;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Core.Domain.Entities;

public class WalletItem : EntityBase<WalletItemModel, WalletItem>
{
    public decimal Amount { get; private set; }
    public decimal BuyPrice { get; private set; }
    public DateTime BuyDate { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DateUpdated { get; private set; }
    public string ExtTicker { get; private set; }
    public Guid ExtMarketItemGuid { get; private set; }

    #region Domain child construction instance

    private WalletItem(IDomainEntityFactory factoryBase,
    IDefaultMapper<WalletItemModel, WalletItem> mapperBase,
    ILogger<WalletItem> loggerBase) : base(factoryBase, mapperBase, loggerBase)
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
    public WalletItem(
        IDomainEntityFactory factory,
        IDefaultMapper<CreateWalletItemModel, WalletItem> createMapper,
        IDefaultMapper<WalletItemModel, WalletItem> mapper,
        ILogger<WalletItem> logger,
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
    public WalletItem(
        IDomainEntityFactory factory,
        IDefaultMapper<WalletItemModel, WalletItem> mapper,
        ILogger<WalletItem> logger,
        WalletItemModel model) : this(factory, mapper, logger)
    {
        if (model.Amount <= 0)
        {
            throw new DomainValidationException<WalletItem>($"{nameof(model.Amount)} is not valid");
        }
        if (model.DateUpdated.Equals(DateTime.MinValue))
        {
            throw new DomainValidationException<WalletItem>($"{nameof(model.DateUpdated)} is not valid");
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
                throw new DomainValidationException<WalletItem>($"{nameof(model.BuyPrice)} is not valid");
            }
            if (model.Quantity <= 0)
            {
                throw new DomainValidationException<WalletItem>($"{nameof(model.Quantity)} is not valid");
            }
            if (model.BuyDate.Equals(DateTime.MinValue))
            {
                throw new DomainValidationException<WalletItem>($"{nameof(model.BuyDate)} is not valid");
            }
        }
        else
        {
            throw new DomainValidationException<WalletItem>($"{nameof(modelBase)} is not valid");
        }
    }

    #endregion

    public override WalletItemModel ToModel()
    {
        var toModel = MapperBase.Map(this);

        return toModel;
    }
}