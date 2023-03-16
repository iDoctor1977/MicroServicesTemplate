using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Address;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;

namespace CoreServicesTemplate.StorageRoom.Core.CustomMappers;

public sealed class UserCreateCoreCustomMapper : CustomMapperBase<UserAppModel, CreateUserAggModel>
{
    private readonly IDefaultMapper<AddressAppModel, CreateAddressAggModel> _addressMapper;

    public UserCreateCoreCustomMapper(
        IDefaultMapper<UserAppModel, CreateUserAggModel> userMapper, 
        IDefaultMapper<AddressAppModel, CreateAddressAggModel> addressMapper) : base(userMapper)
    {
        _addressMapper = addressMapper;
    }

    public override CreateUserAggModel Map(UserAppModel @in)
    {
        var aggModel = base.Map(@in);
        aggModel.AddressAggModel = _addressMapper.Map(@in.AddressAppModel);

        return aggModel;
    }

    public override UserAppModel Map(CreateUserAggModel @out)
    {
        var appModel = base.Map(@out);
        appModel.AddressAppModel = _addressMapper.Map(@out.AddressAggModel);

        return appModel;
    }
}