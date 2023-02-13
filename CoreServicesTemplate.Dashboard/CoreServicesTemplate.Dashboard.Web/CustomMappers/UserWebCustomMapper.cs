using System.Globalization;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Extensions;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers
{
    public class UserWebCustomMapper : CustomMapperBase<UserViewModel, UserAppModel>
    {
        private readonly IDefaultMapper<AddressViewModel, AddressAppModel> _addressMapper;

        public UserWebCustomMapper(
            IDefaultMapper<AddressViewModel, AddressAppModel> addressMapper,
            IDefaultMapper<UserViewModel, UserAppModel> userMapper) : base(userMapper)
        {
            _addressMapper = addressMapper;
        }

        public override UserAppModel Map(UserViewModel @in)
        {
            var appModel = base.Map(@in);
            appModel.Birth = DateTime.ParseExact(@in.Birth, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            appModel.AddressAppModel = _addressMapper.Map(@in.AddressViewModel);

            return appModel;
        }

        public override UserViewModel Map(UserAppModel @out)
        {
            var viewModel = base.Map(@out);
            viewModel.Birth = @out.Birth.ToStandardString();
            viewModel.AddressViewModel = _addressMapper.Map(@out.AddressAppModel);

            return viewModel;
        }
    }
}