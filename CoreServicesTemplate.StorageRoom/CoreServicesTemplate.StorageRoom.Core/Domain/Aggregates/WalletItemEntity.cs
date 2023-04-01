using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;

public class WalletItemEntity
{
    public Guid Guid { get; private set; }
    public decimal Amount { get; private set; }
    public decimal BuyPrice { get; private set; }
    public DateTime BuyDate { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DateUpdated { get; private set; }
    public string ExtTicker { get; private set; }
    public Guid ExtMarketItemGuid { get; private set; }

    private readonly IDefaultMapper<CreateWalletItemModel, WalletItemEntity> _createWalletItemMapper;
    private readonly IDefaultMapper<WalletItemModel, WalletItemEntity> _walletItemMapper;

    #region Aggregate construction instance

    private WalletItemEntity(
    IDefaultMapper<CreateWalletItemModel, WalletItemEntity> createWalletItemMapper,
    IDefaultMapper<WalletItemModel, WalletItemEntity> walletItemMapper)
    {
        _createWalletItemMapper = createWalletItemMapper;
        _walletItemMapper = walletItemMapper;
    }

    /// <summary>
    /// For creation instance
    /// </summary>
    /// <param name="createWalletItemMapper"></param>
    /// <param name="walletItemMapper"></param>
    /// <param name="model"></param>
    public WalletItemEntity(
        IDefaultMapper<CreateWalletItemModel, WalletItemEntity> createWalletItemMapper,
        IDefaultMapper<WalletItemModel, WalletItemEntity> walletItemMapper,
        CreateWalletItemModel model) : this(createWalletItemMapper, walletItemMapper)
    {
        SharedConstruction(model);

        _createWalletItemMapper.Map(model, this);

        Guid = Guid.NewGuid();
    }

    /// <summary>
    /// For updating instance
    /// </summary>
    /// <param name="createWalletItemMapper"></param>
    /// <param name="walletItemMapper"></param>
    /// <param name="model"></param>
    /// <exception cref="DomainValidationException{T}"></exception>
    public WalletItemEntity(
        IDefaultMapper<CreateWalletItemModel, WalletItemEntity> createWalletItemMapper,
        IDefaultMapper<WalletItemModel, WalletItemEntity> walletItemMapper,
        WalletItemModel model) : this(createWalletItemMapper, walletItemMapper)
    {
        if (model.ExtTicker.Equals(null))
        {
            throw new DomainValidationException<WalletItemEntity>("Ticker is not valid");
        }
        if (model.Guid.Equals(null) || model.Guid == Guid.Empty)
        {
            throw new DomainValidationException<WalletItemEntity>("Guid is not valid");
        }
        if (model.Amount <= 0)
        {
            throw new DomainValidationException<WalletItemEntity>("Amount is not valid");
        }
        if (model.DateUpdated.Equals(DateTime.MinValue))
        {
            throw new DomainValidationException<WalletItemEntity>("Date updated is not valid");
        }

        SharedConstruction(model);

        _walletItemMapper.Map(model, this);
    }

    private void SharedConstruction(WalletItemModelBase model)
    {
        if (model.ExtMarketItemGuid.Equals(null) || model.ExtMarketItemGuid == Guid.Empty)
        {
            throw new DomainValidationException<WalletItemEntity>("Market item guid is not valid");
        }
        if (model.BuyPrice <= 0)
        {
            throw new DomainValidationException<WalletItemEntity>("Buy price is not valid");
        }
        if (model.Quantity <= 0)
        {
            throw new DomainValidationException<WalletItemEntity>("Quantity is not valid");
        }
        if (model.BuyDate.Equals(DateTime.MinValue))
        {
            throw new DomainValidationException<WalletItemEntity>("Buy date is not valid");
        }
    }

    #endregion

    public WalletItemModel ToWalletItemModel()
    {
        var toModel = _walletItemMapper.Map(this);

        return toModel;
    }
}