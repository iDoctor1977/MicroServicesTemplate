using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.Consolidators;

public class UsersApiCustomConsolidator : AConsolidatorBase<UsersApiModel, UsersModel>,
    IConsolidatorToResolve<UsersApiModel, UsersModel>,
    IConsolidatorToResolveReversing<UsersApiModel, UsersModel>
{
    private readonly IConsolidatorToData<UserApiModel, UserModel> _userConsolidator;

    private UsersApiModel _usersApiModel;
    private UsersModel _usersModel;

    public UsersApiCustomConsolidator(ICustomMapper customMapper, IConsolidatorToData<UserApiModel, UserModel> userConsolidator) : base(customMapper)
    {
        _userConsolidator = userConsolidator;

        _usersApiModel = new UsersApiModel
        {
            UsersApiModelList = new List<UserApiModel>()
        };

        _usersModel = new UsersModel
        {
            UsersModelList = new List<UserModel>()
        };
    }

    public override IConsolidatorToResolve<UsersApiModel, UsersModel> ToData(UsersApiModel @in)
    {
        _usersApiModel = @in;
        _usersModel = InDataToOutData(@in);

        var modelList = new List<UserModel>();
        foreach (var modelIn in @in.UsersApiModelList)
        {
            modelList.Add(_userConsolidator.ToData(modelIn).Resolve());
        }

        _usersModel.UsersModelList = modelList;

        return this;
    }

    public override IConsolidatorToResolveReversing<UsersApiModel, UsersModel> ToDataReverse(UsersModel @out)
    {
        _usersApiModel = OutDataToInData(_usersModel);

        var modelList = new List<UserApiModel>();
        foreach (var userModel in _usersModel.UsersModelList)
        {
            modelList.Add(_userConsolidator.ToDataReverse(userModel).Resolve());
        }

        _usersApiModel.UsersApiModelList = modelList;

        return this;
    }

    UsersModel IConsolidatorToResolve<UsersApiModel, UsersModel>.Resolve()
    {
        return _usersModel;
    }

    UsersApiModel IConsolidatorToResolveReversing<UsersApiModel, UsersModel>.Resolve()
    {
        return _usersApiModel;
    }
}