using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.ResolveMappers;

public sealed class UserCoreCustomResolveMapper : ACustomResolveMapperBase<UserAppModel, UserAggModel>
{
    private readonly IResolveMapper<AddressAppModel, AddressAggModel> _addressConsolidator;

    public UserCoreCustomResolveMapper(ICustomMapper customMapper, IResolveMapper<AddressAppModel, AddressAggModel> addressConsolidator) : base(customMapper)
    {
        _addressConsolidator = addressConsolidator;

        ModelIn = new UserAppModel();
        ModelOut = new UserAggModel();
    }

    public override IResolveMapperToResolve<UserAppModel, UserAggModel> ToData(UserAppModel @in)
    {
        ModelIn = @in;
        ModelIn.AddressAppModel = @in.AddressAppModel;

        ModelOut = InDataToOutData(@in);
        ModelOut.AddressAggModel = _addressConsolidator.ToData(@in.AddressAppModel).Resolve();

        return this;
    }

    public override IResolveMapperToResolveReversing<UserAppModel, UserAggModel> ToDataReverse(UserAggModel @out)
    {
        ModelIn = OutDataToInData(ModelOut);
        ModelIn.AddressAppModel = _addressConsolidator.ToDataReverse(ModelOut.AddressAggModel).Resolve();

        return this;
    }
}