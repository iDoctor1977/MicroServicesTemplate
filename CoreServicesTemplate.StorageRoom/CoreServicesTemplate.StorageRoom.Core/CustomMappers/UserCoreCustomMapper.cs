using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.CustomMappers;

public sealed class UserCoreCustomMapper : ACustomMapperBase<UserAppModel, UserAggModel>
{
    private readonly IMapping<AddressAppModel, AddressAggModel> _addressMapper;

    public UserCoreCustomMapper(IMapperWrap mapper, IMapping<AddressAppModel, AddressAggModel> addressMapper) : base(mapper)
    {
        _addressMapper = addressMapper;
    }

    public override UserAggModel Map(UserAppModel @in)
    {
        var aggModel = DataInToDataOut(@in);
        aggModel.AddressAggModel = _addressMapper.Map(@in.AddressAppModel);

        return aggModel;
    }

    public override UserAppModel Map(UserAggModel @out)
    {
        var appModel = DataOutToDataIn(@out);
        appModel.AddressAppModel = _addressMapper.Map(@out.AddressAggModel);

        return appModel;
    }

    public override UserAggModel Map(UserAppModel @in, UserAggModel @out)
    {
        throw new NotImplementedException();
    }
}