using System.Globalization;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Dashboard.Web.Receivers
{
    public class AddUserCustomReceiver : AConsolidatorBase<UserViewModel, UserModel>
    {
        public AddUserCustomReceiver(ICustomMapper customMapper) : base(customMapper) { }

        public override UserModel ToData(UserViewModel viewModel)
        {
            var model = ToExternalData(viewModel);
            model.Birth = DateTime.ParseExact(viewModel.Birth, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            return model;
        }
    }
}