using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;

public class WalletItemAggregate
{
    public Guid Guid { get; private set; }
    public decimal Amount { get; private set; }
    public decimal BuyPrice { get; private set; }
    public DateTime BuyDate { get; private set; }
    public int Quantity { get; private set; }
    public DateTime DateUpdated { get; private set; }

    private readonly IDefaultMapper<CreateWalletItemModel, WalletItemAggregate> _createWalletItemMapper;
    private readonly IDefaultMapper<WalletItemModel, WalletItemAggregate> _walletItemMapper;

    #region Aggregate construction instance

    private WalletItemAggregate(
    IDefaultMapper<CreateWalletItemModel, WalletItemAggregate> createWalletItemMapper,
    IDefaultMapper<WalletItemModel, WalletItemAggregate> walletItemMapper)
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
    public WalletItemAggregate(
        IDefaultMapper<CreateWalletItemModel, WalletItemAggregate> createWalletItemMapper,
        IDefaultMapper<WalletItemModel, WalletItemAggregate> walletItemMapper,
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
    public WalletItemAggregate(
        IDefaultMapper<CreateWalletItemModel, WalletItemAggregate> createWalletItemMapper,
        IDefaultMapper<WalletItemModel, WalletItemAggregate> walletItemMapper,
        WalletItemModel model) : this(createWalletItemMapper, walletItemMapper)
    {
        if (model.Guid.Equals(null) || model.Guid == Guid.Empty)
        {
            throw new DomainValidationException<WalletItemAggregate>("Guid is not valid");
        }
        if (model.Amount <= 0)
        {
            throw new DomainValidationException<WalletItemAggregate>("Amount is not valid");
        }
        if (model.DateUpdated.Equals(DateTime.MinValue))
        {
            throw new DomainValidationException<WalletItemAggregate>("Date updated is not valid");
        }

        SharedConstruction(model);

        _walletItemMapper.Map(model, this);
    }

    private void SharedConstruction(BaseWalletItemModel model)
    {
        if (model.BuyPrice <= 0)
        {
            throw new DomainValidationException<WalletItemAggregate>("Buy price is not valid");
        }
        if (model.Quantity <= 0)
        {
            throw new DomainValidationException<WalletItemAggregate>("Quantity is not valid");
        }
        if (model.BuyDate.Equals(DateTime.MinValue))
        {
            throw new DomainValidationException<WalletItemAggregate>("Buy date is not valid");
        }
    }

    #endregion

    public WalletItemModel ToWalletItemModel()
    {
        var toModel = _walletItemMapper.Map(this);

        return toModel;
    }
}