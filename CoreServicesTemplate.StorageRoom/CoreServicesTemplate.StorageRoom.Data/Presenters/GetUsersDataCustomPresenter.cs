using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;

namespace CoreServicesTemplate.StorageRoom.Data.Presenters
{
    public class GetUsersDataCustomPresenter : AConsolidatorBase<IEnumerable<User>, UsersModel>
    {
        private readonly IConsolidators<User, UserModel> _customPresenter;

        public GetUsersDataCustomPresenter(ICustomMapper customMapper, IConsolidators<User, UserModel> customPresenter) : base(customMapper)
        {
            _customPresenter = customPresenter;
        }

        public override UsersModel ToData(IEnumerable<User> entity)
        {
            var model = ToExternalData(entity);
            model.UsersModelList = _customPresenter.ToData(entity);

            return model;
        }
    }
}