using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Consolidators;

public sealed class UserCoreCustomConsolidator : ACustomConsolidatorBase<UserAppModel, UserAggModel>
{
    private readonly IConsolidator<AddressAppModel, AddressAggModel> _addressConsolidator;

    public UserCoreCustomConsolidator(ICustomMapper customMapper, IConsolidator<AddressAppModel, AddressAggModel> addressConsolidator) : base(customMapper)
    {
        _addressConsolidator = addressConsolidator;

        ModelIn = new UserAppModel();
        ModelOut = new UserAggModel();
    }

    public override IConsolidatorToResolve<UserAppModel, UserAggModel> ToData(UserAppModel @in)
    {
        ModelIn = @in;
        ModelIn.AddressAppModel = @in.AddressAppModel;

        ModelOut = InDataToOutData(@in);
        ModelOut.AddressAggModel = _addressConsolidator.ToData(@in.AddressAppModel).Resolve();

        return this;
    }

    public override IConsolidatorToResolveReversing<UserAppModel, UserAggModel> ToDataReverse(UserAggModel @out)
    {
        ModelIn = OutDataToInData(ModelOut);
        ModelIn.AddressAppModel = _addressConsolidator.ToDataReverse(ModelOut.AddressAggModel).Resolve();

        return this;
    }
}