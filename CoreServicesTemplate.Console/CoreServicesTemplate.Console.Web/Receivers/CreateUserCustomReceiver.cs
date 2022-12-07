using System;
using System.Globalization;
using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Console.Web.Receivers
{
    public class CreateUserCustomReceiver : AConsolidatorBase<UserViewModel, UserModel>
    {
        public CreateUserCustomReceiver(ICustomMapper customMapper) : base(customMapper) { }

        public override UserModel ToData(UserViewModel viewModel)
        {
            var model = ToExternalData(viewModel);
            model.Birth = DateTime.ParseExact(viewModel.Birth, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            return model;
        }
    }
}