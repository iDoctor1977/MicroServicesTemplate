using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers
{
    public class UsersWebCustomMapper : ACustomMapperBase<UsersViewModel, UsersAppModel>
    {
        private readonly IMapperService<UserViewModel, UserAppModel> _userMapper;

        public UsersWebCustomMapper(IMapperWrap mapperWrap, IMapperService<UserViewModel, UserAppModel> userMapper) : base(mapperWrap)
        {
            _userMapper = userMapper;
        }

        public override UsersAppModel Map(UsersViewModel @in)
        {
            var appModel = DataInToDataOut(@in);

            var modelList = new List<UserAppModel>();
            foreach (var modelIn in @in.UsersViewModelList)
            {
                modelList.Add(_userMapper.Map(modelIn));
            }

            appModel.UsersModelList = modelList;

            return appModel;
        }

        public override UsersViewModel Map(UsersAppModel @out)
        {
            var viewModel = DataOutToDataIn(@out);

            var modelList = new List<UserViewModel>();
            foreach (var userModel in @out.UsersModelList)
            {
                modelList.Add(_userMapper.Map(userModel));
            }

            viewModel.UsersViewModelList = modelList;

            return viewModel;
        }

        public override UsersAppModel Map(UsersViewModel @in, UsersAppModel @out)
        {
            throw new NotImplementedException();
        }
    }
}