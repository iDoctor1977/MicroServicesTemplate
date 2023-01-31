using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.ResolveMappers;

public sealed class UserApiCustomResolveMapper : ACustomResolveMapperBase<UserApiModel, UserAppModel>
{
    private readonly IResolveMapper<AddressApiModel, AddressAppModel> _addressConsolidator;

    public UserApiCustomResolveMapper(ICustomMapper customMapper, IResolveMapper<AddressApiModel, AddressAppModel> addressConsolidator) : base(customMapper)
    {
        _addressConsolidator = addressConsolidator;

        ModelIn = new UserApiModel();
        ModelOut = new UserAppModel();
    }

    public override IResolveMapperToResolve<UserApiModel, UserAppModel> ToData(UserApiModel @in)
    {
        ModelIn = @in;
        ModelIn.AddressApiModel = @in.AddressApiModel;

        ModelOut = InDataToOutData(@in);
        ModelOut.AddressAppModel = _addressConsolidator.ToData(@in.AddressApiModel).Resolve();

        return this;
    }

    public override IResolveMapperToResolveReversing<UserApiModel, UserAppModel> ToDataReverse(UserAppModel @out)
    {
        ModelIn = OutDataToInData(ModelOut);
        ModelIn.AddressApiModel = _addressConsolidator.ToDataReverse(ModelOut.AddressAppModel).Resolve();

        return this;
    }
}