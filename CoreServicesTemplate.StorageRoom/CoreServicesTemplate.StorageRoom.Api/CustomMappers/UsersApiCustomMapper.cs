using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;

namespace CoreServicesTemplate.StorageRoom.Api.CustomMappers;

public sealed class UsersApiCustomMapper : CustomMapperBase<UsersApiModel, UsersAppModel>
{
    private readonly IDefaultMapper<UserApiModel, UserAppModel> _userMapper;

    public UsersApiCustomMapper(
        IDefaultMapper<UsersApiModel, UsersAppModel> usersMapper, 
        IDefaultMapper<UserApiModel, UserAppModel> userMapper) : base(usersMapper)
    {
        _userMapper = userMapper;
    }

    public override UsersAppModel Map(UsersApiModel @in)
    {
        var appModel = base.Map(@in);

        var modelList = new List<UserAppModel>();
        foreach (var modelIn in @in.UsersApiModelList)
        {
            modelList.Add(_userMapper.Map(modelIn));
        }

        appModel.UsersModelList = modelList;

        return appModel;
    }

    public override UsersApiModel Map(UsersAppModel @out)
    {
        var apiModel = base.Map(@out);

        var modelList = new List<UserApiModel>();
        foreach (var userModel in @out.UsersModelList)
        {
            modelList.Add(_userMapper.Map(userModel));
        }

        apiModel.UsersApiModelList = modelList;

        return apiModel;
    }

    public override UsersAppModel Map(UsersApiModel @in, UsersAppModel @out)
    {
        var appModel = base.Map(@in, @out);

        var modelList = new List<UserAppModel>();
        foreach (var modelIn in @in.UsersApiModelList)
        {
            modelList.Add(_userMapper.Map(modelIn));
        }

        appModel.UsersModelList = modelList;

        return appModel;
    }
}