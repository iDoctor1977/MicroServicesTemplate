using System.Collections.Generic;
using System.Linq;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Consolidators
{
    public sealed class UsersDataCustomConsolidator : AConsolidatorBase<UsersModel, IEnumerable<User>>,
        IConsolidatorToResolve<UsersModel, IEnumerable<User>>,
        IConsolidatorToResolveReversing<UsersModel, IEnumerable<User>>

    {
        private readonly IConsolidator<UserModel, User> _userConsolidator;

        private UsersModel _usersModel;
        private IEnumerable<User> _enumerableUsers;

        public UsersDataCustomConsolidator(ICustomMapper customMapper, IConsolidator<UserModel, User> userConsolidator) : base(customMapper)
        {
            _userConsolidator = userConsolidator;

            _usersModel = new UsersModel
            {
                UsersModelList = new List<UserModel>()
            };

            _enumerableUsers = new List<User>();
        }
        
        public override IConsolidatorToResolve<UsersModel, IEnumerable<User>> ToData(UsersModel @in)
        {
            _usersModel = @in;
            _enumerableUsers = InDataToOutData(@in);

            var list = new List<User>();
            foreach (var modelIn in @in.UsersModelList)
            {
                list.Add(_userConsolidator.ToData(modelIn).Resolve());
            }

            _enumerableUsers = list;

            return this;
        }

        public override IConsolidatorToResolveReversing<UsersModel, IEnumerable<User>> ToDataReverse(IEnumerable<User> @out)
        {
            var list = new List<UserModel>();
            var enumerable = @out.ToList();
            foreach (var model in enumerable)
            {
                list.Add(_userConsolidator.ToDataReverse(model).Resolve());
            }

            _usersModel.UsersModelList = list;

            return this;
        }
        
        IEnumerable<User> IConsolidatorToResolve<UsersModel, IEnumerable<User>>.Resolve()
        {
            return _enumerableUsers;
        }

        UsersModel IConsolidatorToResolveReversing<UsersModel, IEnumerable<User>>.Resolve()
        {
            return _usersModel;
        }
    }
}