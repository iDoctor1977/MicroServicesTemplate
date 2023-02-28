using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.AppModels;

namespace CoreServicesTemplate.StorageRoom.Api.CustomMappers;

public class UserApiCustomMapper : CustomMapperBase<UserApiModel, UserAppModel>
{
    private readonly IDefaultMapper<AddressApiModel, AddressAppModel> _addressMapper;

    public UserApiCustomMapper(
        IDefaultMapper<AddressApiModel, AddressAppModel> addressMapper,
        IDefaultMapper<UserApiModel, UserAppModel> userMapper) : base(userMapper)
    {
        _addressMapper = addressMapper;
    }

    public override UserAppModel Map(UserApiModel @in)
    {
        var appModel = base.Map(@in);
        appModel.AddressAppModel = _addressMapper.Map(@in.AddressApiModel);

        return appModel;
    }

    public override UserApiModel Map(UserAppModel @out)
    {
        var appModel = base.Map(@out);
        appModel.AddressApiModel = _addressMapper.Map(@out.AddressAppModel);

        return appModel;
    }

    public override UserAppModel Map(UserApiModel @in, UserAppModel @out)
    {
        var appModel = base.Map(@in, @out);
        appModel.AddressAppModel = _addressMapper.Map(@in.AddressApiModel, appModel.AddressAppModel);

        return appModel;
    }

    public override UserApiModel Map(UserAppModel @out, UserApiModel @in)
    {
        var apiModel = base.Map(@out, @in);
        apiModel.AddressApiModel = _addressMapper.Map(@out.AddressAppModel, apiModel.AddressApiModel);

        return apiModel;
    }
}