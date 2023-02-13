using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers
{
    public class UsersWebCustomMapper : CustomMapperBase<UsersViewModel, UsersAppModel>
    {
        private readonly IDefaultMapper<UserViewModel, UserAppModel> _userMapper;

        public UsersWebCustomMapper(
            IDefaultMapper<UsersViewModel, UsersAppModel> usersMapper, 
            IDefaultMapper<UserViewModel, UserAppModel> userMapper) : base(usersMapper)
        {
            _userMapper = userMapper;
        }

        public override UsersAppModel Map(UsersViewModel @in)
        {
            var appModel = base.Map(@in);

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
            var viewModel = base.Map(@out);

            var modelList = new List<UserViewModel>();
            foreach (var userModel in @out.UsersModelList)
            {
                modelList.Add(_userMapper.Map(userModel));
            }

            viewModel.UsersViewModelList = modelList;

            return viewModel;
        }

    }
}