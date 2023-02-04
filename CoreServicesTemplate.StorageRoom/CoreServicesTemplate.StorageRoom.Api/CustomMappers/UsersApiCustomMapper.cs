using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.CustomMappers;

public sealed class UsersApiCustomMapper : ICustomMapper<UsersApiModel, UsersAppModel>
{
    private readonly IDefaultMapper<UsersApiModel, UsersAppModel> _usersMapper;
    private readonly IDefaultMapper<UserApiModel, UserAppModel> _userMapper;

    public UsersApiCustomMapper(
        IDefaultMapper<UsersApiModel, UsersAppModel> usersMapper, 
        IDefaultMapper<UserApiModel, UserAppModel> userMapper)
    {
        _usersMapper = usersMapper;
        _userMapper = userMapper;
    }

    public UsersAppModel Map(UsersApiModel @in)
    {
        var appModel = _usersMapper.Map(@in);

        var modelList = new List<UserAppModel>();
        foreach (var modelIn in @in.UsersApiModelList)
        {
            modelList.Add(_userMapper.Map(modelIn));
        }

        appModel.UsersModelList = modelList;

        return appModel;
    }

    public UsersApiModel Map(UsersAppModel @out)
    {
        var apiModel = _usersMapper.Map(@out);

        var modelList = new List<UserApiModel>();
        foreach (var userModel in @out.UsersModelList)
        {
            modelList.Add(_userMapper.Map(userModel));
        }

        apiModel.UsersApiModelList = modelList;

        return apiModel;
    }

    public UsersAppModel Map(UsersApiModel @in, UsersAppModel @out)
    {
        var appModel = _usersMapper.Map(@in, @out);

        var modelList = new List<UserAppModel>();
        foreach (var modelIn in @in.UsersApiModelList)
        {
            modelList.Add(_userMapper.Map(modelIn));
        }

        appModel.UsersModelList = modelList;

        return appModel;
    }

    public UsersApiModel Map(UsersAppModel @out, UsersApiModel @in)
    {
        throw new NotImplementedException();
    }
}