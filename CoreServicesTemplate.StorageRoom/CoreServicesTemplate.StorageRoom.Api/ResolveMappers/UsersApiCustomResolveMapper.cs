using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.ResolveMappers;

public sealed class UsersApiCustomResolveMapper : ACustomResolveMapperBase<UsersApiModel, UsersAppModel>
{
    private readonly IResolveMapper<UserApiModel, UserAppModel> _userConsolidator;

    public UsersApiCustomResolveMapper(ICustomMapper customMapper, IResolveMapper<UserApiModel, UserAppModel> userConsolidator) : base(customMapper)
    {
        _userConsolidator = userConsolidator;

        ModelIn.UsersApiModelList = new List<UserApiModel>();
        ModelOut.UsersModelList = new List<UserAppModel>();
    }

    public override IResolveMapperToResolve<UsersApiModel, UsersAppModel> ToData(UsersApiModel @in)
    {
        ModelIn = @in;
        ModelOut = InDataToOutData(@in);

        var modelList = new List<UserAppModel>();
        foreach (var modelIn in @in.UsersApiModelList)
        {
            modelList.Add(_userConsolidator.ToData(modelIn).Resolve());
        }

        ModelOut.UsersModelList = modelList;

        return this;
    }

    public override IResolveMapperToResolveReversing<UsersApiModel, UsersAppModel> ToDataReverse(UsersAppModel @out)
    {
        ModelIn = OutDataToInData(ModelOut);

        var modelList = new List<UserApiModel>();
        foreach (var userModel in @out.UsersModelList)
        {
            modelList.Add(_userConsolidator.ToDataReverse(userModel).Resolve());
        }

        ModelIn.UsersApiModelList = modelList;

        return this;
    }
}