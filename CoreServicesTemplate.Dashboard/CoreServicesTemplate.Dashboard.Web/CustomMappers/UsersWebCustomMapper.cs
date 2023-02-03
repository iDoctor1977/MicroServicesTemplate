using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.CustomMappers
{
    public class UsersWebCustomMapper : ICustomMapper<UsersViewModel, UsersAppModel>
    {
        private readonly IDefaultMapper<UsersViewModel, UsersAppModel> _usersMapper;
        private readonly IDefaultMapper<UserViewModel, UserAppModel> _userMapper;

        public UsersWebCustomMapper(
            IDefaultMapper<UserViewModel, UserAppModel> userMapper, 
            IDefaultMapper<UsersViewModel, UsersAppModel> usersMapper)
        {
            _usersMapper = usersMapper;
            _userMapper = userMapper;
        }

        public UsersAppModel Map(UsersViewModel @in)
        {
            var appModel = _usersMapper.Map(@in);

            var modelList = new List<UserAppModel>();
            foreach (var modelIn in @in.UsersViewModelList)
            {
                modelList.Add(_userMapper.Map(modelIn));
            }

            appModel.UsersModelList = modelList;

            return appModel;
        }

        public UsersViewModel Map(UsersAppModel @out)
        {
            var viewModel = _usersMapper.Map(@out);

            var modelList = new List<UserViewModel>();
            foreach (var userModel in @out.UsersModelList)
            {
                modelList.Add(_userMapper.Map(userModel));
            }

            viewModel.UsersViewModelList = modelList;

            return viewModel;
        }

        public UsersAppModel Map(UsersViewModel @in, UsersAppModel @out)
        {
            throw new NotImplementedException();
        }
    }
}