using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.Presenters
{
    public class GetUsersCoreCustomPresenter : AConsolidatorBase<UsersModel, UsersApiModel>
    {
        private readonly IConsolidators<UserModel, UserApiModel> _customPresenter;

        public GetUsersCoreCustomPresenter(ICustomMapper customMapper, IConsolidators<UserModel, UserApiModel> customPresenter) : base(customMapper)
        {
            _customPresenter = customPresenter;
        }

        public override UsersApiModel ToData(UsersModel model)
        {
            var viewModel = ToExternalData(model);
            viewModel.UsersApiModelList = _customPresenter.ToData(model.UsersModelList);

            return viewModel;
        }
    }
}