using System.Collections.Generic;
using System.Linq;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Presenters
{
    public class UsersDataCustomPresenter : AConsolidatorBase<IEnumerable<User>, UsersModel>
    {
        private readonly IConsolidators<User, UserModel> _userConsolidator;

        public UsersDataCustomPresenter(ICustomMapper customMapper, IConsolidators<User, UserModel> userConsolidator) : base(customMapper)
        {
            _userConsolidator = userConsolidator;
        }

        public override UsersModel ToData(IEnumerable<User> modelIn)
        {
            var entityList = modelIn.ToList();
            var model = ToExternalData(entityList);
            model.UsersModelList = _userConsolidator.ToData(entityList);

            return model;
        }
    }
}