using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Extensions;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Web.Presenters
{
    public class ReadUserCustomPresenter : AConsolidatorBase<UserApiModel, UserViewModel>
    {
        public ReadUserCustomPresenter(ICustomMapper customMapper) : base(customMapper) { }

        public override UserViewModel ToData(UserApiModel model)
        {
            var viewModel = ToExternalData(model);
            viewModel.Birth = model.Birth.ToStandardString();

            return viewModel;
        }
    }
}