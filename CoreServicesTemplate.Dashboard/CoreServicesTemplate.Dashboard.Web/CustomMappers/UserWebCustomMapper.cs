using System.Globalization;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Extensions;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers
{
    public class UserWebCustomMapper : ICustomMapper<UserViewModel, UserAppModel>
    {
        private readonly IDefaultMapper<UserViewModel, UserAppModel> _userMapper;
        private readonly IDefaultMapper<AddressViewModel, AddressAppModel> _addressMapper;

        public UserWebCustomMapper(
            IDefaultMapper<AddressViewModel, AddressAppModel> addressMapper,
            IDefaultMapper<UserViewModel, UserAppModel> userMapper)
        {
            _userMapper = userMapper;
            _addressMapper = addressMapper;
        }

        public  UserAppModel Map(UserViewModel @in)
        {
            var appModel = _userMapper.Map(@in);
            appModel.Birth = DateTime.ParseExact(@in.Birth, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            appModel.AddressAppModel = _addressMapper.Map(@in.AddressViewModel);

            return appModel;
        }

        public  UserViewModel Map(UserAppModel @out)
        {
            var viewModel = _userMapper.Map(@out);
            viewModel.Birth = @out.Birth.ToStandardString();
            viewModel.AddressViewModel = _addressMapper.Map(@out.AddressAppModel);

            return viewModel;
        }

        public  UserAppModel Map(UserViewModel @in, UserAppModel @out)
        {
            throw new NotImplementedException();
        }

        public UserViewModel Map(UserAppModel @out, UserViewModel @in)
        {
            throw new NotImplementedException();
        }
    }
}