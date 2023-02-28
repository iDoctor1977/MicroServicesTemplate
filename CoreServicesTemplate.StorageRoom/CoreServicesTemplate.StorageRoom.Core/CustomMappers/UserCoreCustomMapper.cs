using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.AggModels;
using CoreServicesTemplate.StorageRoom.Common.AppModels;

namespace CoreServicesTemplate.StorageRoom.Core.CustomMappers;

public sealed class UserCoreCustomMapper : CustomMapperBase<UserAppModel, UserAggModel>
{
    private readonly IDefaultMapper<AddressAppModel, AddressAggModel> _addressMapper;

    public UserCoreCustomMapper(
        IDefaultMapper<UserAppModel, UserAggModel> userMapper, 
        IDefaultMapper<AddressAppModel, AddressAggModel> addressMapper) : base(userMapper)
    {
        _addressMapper = addressMapper;
    }

    public override UserAggModel Map(UserAppModel @in)
    {
        var aggModel = base.Map(@in);
        aggModel.AddressAggModel = _addressMapper.Map(@in.AddressAppModel);

        return aggModel;
    }

    public override UserAppModel Map(UserAggModel @out)
    {
        var appModel = base.Map(@out);
        appModel.AddressAppModel = _addressMapper.Map(@out.AddressAggModel);

        return appModel;
    }
}