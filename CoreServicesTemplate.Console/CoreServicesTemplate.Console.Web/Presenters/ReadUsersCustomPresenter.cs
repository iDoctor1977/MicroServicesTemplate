using System;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.Console.Web.Presenters
{
    public class ReadUsersCustomPresenter : AConsolidatorBase<UsersApiModel, UsersViewModel>
    {
        private readonly IConsolidators<UserApiModel, UserViewModel> _readUserCustomPresenter;

        public ReadUsersCustomPresenter(IServiceProvider service) : base(service)
        {
            _readUserCustomPresenter = service.GetRequiredService<IConsolidators<UserApiModel, UserViewModel>>();
        }

        public override UsersViewModel ToData(UsersApiModel model)
        {
            var viewModel = ToExternalData(model);
            viewModel.UsersViewModelList = _readUserCustomPresenter.ToData(model.UsersApiModelList);

            return viewModel;
        }
    }
}