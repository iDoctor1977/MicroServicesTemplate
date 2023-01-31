﻿using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.CustomMappers
{
    public sealed class UsersDataCustomMapper : ACustomMapperBase<UsersAppModel, IEnumerable<User>>
    {
        private readonly IMapping<UserAppModel, User> _userMapper;

        private UsersAppModel _usersModel;
        private IEnumerable<User> _enumerableUsers;

        public UsersDataCustomMapper(IMapperWrap mapper, IMapping<UserAppModel, User> userMapper) : base(mapper)
        {
            _userMapper = userMapper;

            _usersModel = new UsersAppModel
            {
                UsersModelList = new List<UserAppModel>()
            };

            _enumerableUsers = new List<User>();
        }
        
        public override IEnumerable<User> Map(UsersAppModel @in)
        {
            _usersModel = @in;
            _enumerableUsers = DataInToDataOut(@in);

            var list = new List<User>();
            foreach (var modelIn in @in.UsersModelList)
            {
                list.Add(_userMapper.Map(modelIn));
            }

            _enumerableUsers = list;

            return _enumerableUsers;
        }

        public override UsersAppModel Map(IEnumerable<User> @out)
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

        public override IEnumerable<User> Map(UsersAppModel @in, IEnumerable<User> @out)
        {
            throw new NotImplementedException();
        }
    }
}