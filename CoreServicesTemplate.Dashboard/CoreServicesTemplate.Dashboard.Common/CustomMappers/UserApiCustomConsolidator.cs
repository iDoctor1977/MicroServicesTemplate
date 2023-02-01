using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Common.CustomMappers
{
    public class UserApiCustomConsolidator : ACustomMapperBase<UserApiModel, UserAppModel>
    {
        private readonly IMapperService<AddressApiModel, AddressAppModel> _addressMapper;

        public UserApiCustomConsolidator(IMapperWrap mapperWrap, IMapperService<AddressApiModel, AddressAppModel> addressMapper) : base(mapperWrap)
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
            var apiModel = DataOutToDataIn(@out);
            apiModel.AddressApiModel = _addressMapper.Map(@out.AddressAppModel);

            return apiModel;
        }

        public override UserAppModel Map(UserApiModel @in, UserAppModel @out)
        {
            throw new NotImplementedException();
        }
    }
}