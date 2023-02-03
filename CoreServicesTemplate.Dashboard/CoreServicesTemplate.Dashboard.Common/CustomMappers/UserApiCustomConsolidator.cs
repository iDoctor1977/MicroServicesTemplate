using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Common.CustomMappers
{
    public class UserApiCustomConsolidator : ICustomMapper<UserApiModel, UserAppModel>
    {
        private readonly IDefaultMapper<UserApiModel, UserAppModel> _userMapper;
        private readonly IDefaultMapper<AddressApiModel, AddressAppModel> _addressMapper;

        public UserApiCustomConsolidator(IDefaultMapper<AddressApiModel, AddressAppModel> addressMapper, IDefaultMapper<UserApiModel, UserAppModel> userMapper)
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
            var apiModel = _userMapper.Map(@out);
            apiModel.AddressApiModel = _addressMapper.Map(@out.AddressAppModel);

            return apiModel;
        }

        public UserAppModel Map(UserApiModel @in, UserAppModel @out)
        {
            throw new NotImplementedException();
        }
    }
}