using System.Globalization;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Extensions;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers
{
    public class UserWebCustomMapper : ACustomMapperBase<UserViewModel, UserAppModel>
    {
        private readonly IMapperService<AddressViewModel, AddressAppModel> _addressMapper;

        public UserWebCustomMapper(IMapperWrap mapperWrap, IMapperService<AddressViewModel, AddressAppModel> addressMapper) : base(mapperWrap)
        {
            _addressMapper = addressMapper;
        }

        public override UserAppModel Map(UserViewModel @in)
        {
            var appModel = DataInToDataOut(@in);
            appModel.Birth = DateTime.ParseExact(@in.Birth, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            appModel.AddressAppModel = _addressMapper.Map(@in.AddressViewModel);

            return appModel;
        }

        public override UserViewModel Map(UserAppModel @out)
        {
            var viewModel = DataOutToDataIn(@out);
            viewModel.Birth = @out.Birth.ToStandardString();
            viewModel.AddressViewModel = _addressMapper.Map(@out.AddressAppModel);

            return viewModel;
        }

        public override UserAppModel Map(UserViewModel @in, UserAppModel @out)
        {
            throw new NotImplementedException();
        }
    }
}