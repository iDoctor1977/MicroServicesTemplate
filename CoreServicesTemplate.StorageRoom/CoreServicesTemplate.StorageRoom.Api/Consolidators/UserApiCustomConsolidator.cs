using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.Consolidators;

public sealed class UserApiCustomConsolidator : ACustomConsolidatorBase<UserApiModel, UserAppModel>
{
    private readonly IConsolidator<AddressApiModel, AddressAppModel> _addressConsolidator;

    public UserApiCustomConsolidator(ICustomMapper customMapper, IConsolidator<AddressApiModel, AddressAppModel> addressConsolidator) : base(customMapper)
    {
        _addressConsolidator = addressConsolidator;

        ModelIn = new UserApiModel();
        ModelOut = new UserAppModel();
    }

    public override IConsolidatorToResolve<UserApiModel, UserAppModel> ToData(UserApiModel @in)
    {
        ModelIn = @in;
        ModelIn.AddressApiModel = @in.AddressApiModel;

        ModelOut = InDataToOutData(@in);
        ModelOut.AddressAppModel = _addressConsolidator.ToData(@in.AddressApiModel).Resolve();

        return this;
    }

    public override IConsolidatorToResolveReversing<UserApiModel, UserAppModel> ToDataReverse(UserAppModel @out)
    {
        ModelIn = OutDataToInData(ModelOut);
        ModelIn.AddressApiModel = _addressConsolidator.ToDataReverse(ModelOut.AddressAppModel).Resolve();

        return this;
    }
}