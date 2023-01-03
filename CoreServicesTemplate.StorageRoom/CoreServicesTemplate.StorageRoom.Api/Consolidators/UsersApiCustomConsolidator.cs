using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.Consolidators;

public sealed class UsersApiCustomConsolidator : ACustomConsolidatorBase<UsersApiModel, UsersAppModel>
{
    private readonly IConsolidator<UserApiModel, UserAppModel> _userConsolidator;

    public UsersApiCustomConsolidator(ICustomMapper customMapper, IConsolidator<UserApiModel, UserAppModel> userConsolidator) : base(customMapper)
    {
        _userConsolidator = userConsolidator;

        ModelIn.UsersApiModelList = new List<UserApiModel>();
        ModelOut.UsersModelList = new List<UserAppModel>();
    }

    public override IConsolidatorToResolve<UsersApiModel, UsersAppModel> ToData(UsersApiModel @in)
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

    public override IConsolidatorToResolveReversing<UsersApiModel, UsersAppModel> ToDataReverse(UsersAppModel @out)
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