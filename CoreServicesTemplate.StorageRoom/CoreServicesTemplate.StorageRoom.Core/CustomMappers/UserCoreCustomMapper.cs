using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.CustomMappers;

public sealed class UserCoreCustomMapper : ICustomMapper<UserAppModel, UserAggModel>
{
    private readonly IDefaultMapper<UserAppModel, UserAggModel> _userMapper;
    private readonly IDefaultMapper<AddressAppModel, AddressAggModel> _addressMapper;

    public UserCoreCustomMapper(
        IDefaultMapper<UserAppModel, UserAggModel> userMapper, 
        IDefaultMapper<AddressAppModel, AddressAggModel> addressMapper)
    {
        _userMapper = userMapper;
        _addressMapper = addressMapper;
    }

    public UserAggModel Map(UserAppModel @in)
    {
        var aggModel = _userMapper.Map(@in);
        aggModel.AddressAggModel = _addressMapper.Map(@in.AddressAppModel);

        return aggModel;
    }

    public UserAppModel Map(UserAggModel @out)
    {
        var appModel = _userMapper.Map(@out);
        appModel.AddressAppModel = _addressMapper.Map(@out.AddressAggModel);

        return appModel;
    }

    public  UserAggModel Map(UserAppModel @in, UserAggModel @out)
    {
        throw new NotImplementedException();
    }
}