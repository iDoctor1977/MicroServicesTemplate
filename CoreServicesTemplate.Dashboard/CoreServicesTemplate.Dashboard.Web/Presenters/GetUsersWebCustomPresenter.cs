using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Dashboard.Web.Presenters
{
    public class GetUsersWebCustomPresenter : AConsolidatorBase<UsersModel, UsersViewModel>
    {
        private readonly IConsolidators<UserModel, UserViewModel> _customPresenter;

        public GetUsersWebCustomPresenter(ICustomMapper customMapper, IConsolidators<UserModel, UserViewModel> customPresenter) : base(customMapper)
        {
            _customPresenter = customPresenter;
        }

        public override UsersViewModel ToData(UsersModel model)
        {
            var viewModel = ToExternalData(model);
            viewModel.UsersViewModelList = _customPresenter.ToData(model.UsersModelList);

            return viewModel;
        }
    }
}