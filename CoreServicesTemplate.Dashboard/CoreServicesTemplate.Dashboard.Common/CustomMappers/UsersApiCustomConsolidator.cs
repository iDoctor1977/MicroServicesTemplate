using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Common.CustomMappers
{
    public class UsersApiCustomConsolidator : ICustomMapper<UsersApiModel, UsersAppModel>
    {
        private readonly IDefaultMapper<UsersApiModel, UsersAppModel> _usersMapper;
        private readonly IDefaultMapper<UserApiModel, UserAppModel> _userMapper;

        public UsersApiCustomConsolidator(
            IDefaultMapper<UserApiModel, UserAppModel> userMapper, 
            IDefaultMapper<UsersApiModel, UsersAppModel> usersMapper)
        {
            _userMapper = userMapper;
            _usersMapper = usersMapper;
        }

        public UsersAppModel Map(UsersApiModel @in)
        {
            var appModel = _usersMapper.Map(@in);

            var modelList = new List<UserAppModel>();
            foreach (var modelIn in @in.UsersApiModelList)
            {
                modelList.Add(_userMapper.Map(modelIn));
            }

            appModel.UsersModelList = modelList;

            return appModel;
        }

        public UsersApiModel Map(UsersAppModel @out)
        {
            var apiModel = _usersMapper.Map(@out);

            var modelList = new List<UserApiModel>();
            foreach (var userModel in @out.UsersModelList)
            {
                modelList.Add(_userMapper.Map(userModel));
            }

            apiModel.UsersApiModelList = modelList;

            return apiModel;
        }

        public UsersAppModel Map(UsersApiModel @in, UsersAppModel @out)
        {
            var appModel = _usersMapper.Map(@in, @out);

            var modelList = new List<UserAppModel>();
            foreach (var modelIn in @in.UsersApiModelList)
            {
                modelList.Add(_userMapper.Map(modelIn));
            }

            appModel.UsersModelList = modelList;

            return appModel;
        }
    }
}