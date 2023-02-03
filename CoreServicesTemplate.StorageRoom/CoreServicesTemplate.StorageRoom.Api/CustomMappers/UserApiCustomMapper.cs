using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.CustomMappers;

public class UserApiCustomMapper : ICustomMapper<UserApiModel, UserAppModel>
{
    private readonly IDefaultMapper<UserApiModel, UserAppModel> _userMapper;
    private readonly IDefaultMapper<AddressApiModel, AddressAppModel> _addressMapper;

    public UserApiCustomMapper(
        IDefaultMapper<AddressApiModel, AddressAppModel> addressMapper,
        IDefaultMapper<UserApiModel, UserAppModel> userMapper)
    {
        _addressMapper = addressMapper;
        _userMapper = userMapper;
    }

    public UserAppModel Map(UserApiModel @in)
    {
        var appModel = _userMapper.Map(@in);
        appModel.AddressAppModel = _addressMapper.Map(@in.AddressApiModel);

        return appModel;
    }

    public UserApiModel Map(UserAppModel @out)
    {
        var appModel = _userMapper.Map(@out);
        appModel.AddressApiModel = _addressMapper.Map(@out.AddressAppModel);

        return appModel;
    }

    public UserAppModel Map(UserApiModel @in, UserAppModel @out)
    {
        var appModel = _userMapper.Map(@in, @out);
        appModel.AddressAppModel = _addressMapper.Map(@in.AddressApiModel, appModel.AddressAppModel);

        return appModel;
    }
}