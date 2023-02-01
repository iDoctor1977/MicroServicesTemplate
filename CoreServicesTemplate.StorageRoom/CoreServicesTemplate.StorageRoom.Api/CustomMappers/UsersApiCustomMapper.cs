using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.CustomMappers;

public sealed class UsersApiCustomMapper : ACustomMapperBase<UsersApiModel, UsersAppModel>
{
    private readonly IMapperService<UserApiModel, UserAppModel> _userMapper;

    public UsersApiCustomMapper(IMapperWrap mapper, IMapperService<UserApiModel, UserAppModel> userMapper) : base(mapper)
    {
        _userMapper = userMapper;
    }

    public override UsersAppModel Map(UsersApiModel @in)
    {
        var appModel = DataInToDataOut(@in);

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
        var apiModel = DataOutToDataIn(@out);

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
        var appModel = ToDataOut(@in, @out);

        var modelList = new List<UserAppModel>();
        foreach (var modelIn in @in.UsersApiModelList)
        {
            modelList.Add(_userMapper.Map(modelIn));
        }

        appModel.UsersModelList = modelList;

        return appModel;
    }
}