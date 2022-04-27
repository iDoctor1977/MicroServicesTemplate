using System;
using CoreServicesTemplate.Console.Web.Models;

namespace CoreServicesTemplate.Console.Web.Presenters
{
    public class ReadUserCustomPresenter : ABaseConsolidator<UserApiModel, UserViewModel>
    {
        public ReadUserCustomPresenter(IServiceProvider service) : base(service) { }

        public override UserViewModel ToData(UserApiModel model)
        {
            var viewModel = ToExternalData(model);
            viewModel.Birth = model.Birth.ToStandardString();

            return viewModel;
        }
    }
}