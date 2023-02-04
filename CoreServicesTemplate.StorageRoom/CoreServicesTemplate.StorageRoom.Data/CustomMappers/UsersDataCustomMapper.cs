using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.CustomMappers
{
    public sealed class UsersDataCustomMapper : ICustomMapper<UsersAppModel, IEnumerable<User>>
    {
        private readonly IDefaultMapper<UserAppModel, User> _userMapper;
        private readonly IDefaultMapper<UsersAppModel, IEnumerable<User>> _userEntityMapper;

        private UsersAppModel _usersModel;
        private IEnumerable<User> _enumerableUsers;

        public UsersDataCustomMapper(
            IDefaultMapper<UserAppModel, User> userMapper, 
            IDefaultMapper<UsersAppModel, IEnumerable<User>> userEntityMapper)
        {
            _userMapper = userMapper;
            _userEntityMapper = userEntityMapper;

            _usersModel = new UsersAppModel
            {
                UsersModelList = new List<UserAppModel>()
            };

            _enumerableUsers = new List<User>();
        }
        
        public IEnumerable<User> Map(UsersAppModel @in)
        {
            _usersModel = @in;
            _enumerableUsers = _userEntityMapper.Map(@in);

            var list = new List<User>();
            foreach (var modelIn in @in.UsersModelList)
            {
                list.Add(_userMapper.Map(modelIn));
            }

            _enumerableUsers = list;

            return _enumerableUsers;
        }

        public UsersAppModel Map(IEnumerable<User> @out)
        {
            var list = new List<UserAppModel>();
            var enumerable = @out.ToList();
            foreach (var model in enumerable)
            {
                list.Add(_userMapper.Map(model));
            }

            _usersModel.UsersModelList = list;

            return _usersModel;
        }

        public IEnumerable<User> Map(UsersAppModel @in, IEnumerable<User> @out)
        {
            throw new NotImplementedException();
        }

        public UsersAppModel Map(IEnumerable<User> @out, UsersAppModel @in)
        {
            throw new NotImplementedException();
        }
    }
}