using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Consolidators
{
    public sealed class UsersDataCustomConsolidator : AConsolidatorBase<UsersAppModel, IEnumerable<User>>,
        IConsolidatorToResolve<UsersAppModel, IEnumerable<User>>,
        IConsolidatorToResolveReversing<UsersAppModel, IEnumerable<User>>

    {
        private readonly IConsolidator<UserAppModel, User> _userConsolidator;

        private UsersAppModel _usersModel;
        private IEnumerable<User> _enumerableUsers;

        public UsersDataCustomConsolidator(ICustomMapper customMapper, IConsolidator<UserAppModel, User> userConsolidator) : base(customMapper)
        {
            _userConsolidator = userConsolidator;

            _usersModel = new UsersAppModel
            {
                UsersModelList = new List<UserAppModel>()
            };

            _enumerableUsers = new List<User>();
        }
        
        public override IConsolidatorToResolve<UsersAppModel, IEnumerable<User>> ToData(UsersAppModel @in)
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

        public override IConsolidatorToResolveReversing<UsersAppModel, IEnumerable<User>> ToDataReverse(IEnumerable<User> @out)
        {
            var list = new List<UserAppModel>();
            var enumerable = @out.ToList();
            foreach (var model in enumerable)
            {
                list.Add(_userConsolidator.ToDataReverse(model).Resolve());
            }

            _usersModel.UsersModelList = list;

            return this;
        }
        
        IEnumerable<User> IConsolidatorToResolve<UsersAppModel, IEnumerable<User>>.Resolve()
        {
            return _enumerableUsers;
        }

        UsersAppModel IConsolidatorToResolveReversing<UsersAppModel, IEnumerable<User>>.Resolve()
        {
            return _usersModel;
        }
    }
}