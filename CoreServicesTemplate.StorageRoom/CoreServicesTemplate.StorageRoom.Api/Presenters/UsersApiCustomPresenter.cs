using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.Presenters
{
    public class UsersApiCustomPresenter : AConsolidatorBase<UsersModel, UsersApiModel>
    {
        private readonly IConsolidators<UserModel, UserApiModel> _userConsolidator;

        public UsersApiCustomPresenter(ICustomMapper customMapper, IConsolidators<UserModel, UserApiModel> userConsolidator) : base(customMapper)
        {
            _userConsolidator = userConsolidator;
        }

        public override UsersApiModel ToData(UsersModel model)
        {
            var viewModel = ToExternalData(model);
            viewModel.UsersApiModelList = _userConsolidator.ToData(model.UsersModelList);

            return viewModel;
        }
    }
}