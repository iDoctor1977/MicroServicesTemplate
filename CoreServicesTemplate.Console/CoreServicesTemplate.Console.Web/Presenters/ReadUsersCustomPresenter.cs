using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Web.Presenters
{
    public class ReadUsersCustomPresenter : AConsolidatorBase<UsersApiModel, UsersViewModel>
    {
        private readonly IConsolidators<UserApiModel, UserViewModel> _readUserCustomPresenter;

        public ReadUsersCustomPresenter(ICustomMapper customMapper, IConsolidators<UserApiModel, UserViewModel> readUserCustomPresenter) : base(customMapper)
        {
            _readUserCustomPresenter = readUserCustomPresenter;
        }

        public override UsersViewModel ToData(UsersApiModel model)
        {
            var viewModel = ToExternalData(model);
            viewModel.UsersViewModelList = _readUserCustomPresenter.ToData(model.UsersApiModelList);

            return viewModel;
        }
    }
}