using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.CustomMappers;

public class UserApiCustomMapper : ACustomMapperBase<UserApiModel, UserAppModel>
{
    private readonly IMapping<AddressApiModel, AddressAppModel> _addressMapper;

    public UserApiCustomMapper(IMapperWrap mapper, IMapping<AddressApiModel, AddressAppModel> addressMapper) : base(mapper)
    {
        _addressMapper = addressMapper;
    }

    public override UserAppModel Map(UserApiModel @in)
    {
        var appModel = DataInToDataOut(@in);
        appModel.AddressAppModel = _addressMapper.Map(@in.AddressApiModel);

        return appModel;
    }

    public override UserApiModel Map(UserAppModel @out)
    {
        var appModel = DataOutToDataIn(@out);
        appModel.AddressApiModel = _addressMapper.Map(@out.AddressAppModel);

        return appModel;
    }

    public override UserAppModel Map(UserApiModel @in, UserAppModel @out)
    {
        var appModel = ToDataOut(@in, @out);
        appModel.AddressAppModel = _addressMapper.Map(@in.AddressApiModel, appModel.AddressAppModel);

        return appModel;
    }
}