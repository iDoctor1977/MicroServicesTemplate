using System;
using System.Globalization;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Web.Receivers
{
    public class CreateUserCustomReceiver : ABaseConsolidator<UserViewModel, UserApiModel>
    {
        public CreateUserCustomReceiver(IServiceProvider service) : base(service) { }

        public override UserApiModel ToData(UserViewModel viewModel)
        {
            var model = ToExternalData(viewModel);
            model.Birth = DateTime.Parse(viewModel.Birth, CultureInfo.InvariantCulture);

            return model;
        }
    }
}