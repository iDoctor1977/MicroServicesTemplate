using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Extensions;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Console.Web.Presenters
{
    public class GetUserWebCustomPresenter : AConsolidatorBase<UserModel, UserViewModel>
    {
        public GetUserWebCustomPresenter(ICustomMapper customMapper) : base(customMapper) { }

        public override UserViewModel ToData(UserModel model)
        {
            var viewModel = ToExternalData(model);
            viewModel.Birth = model.Birth.ToStandardString();

            return viewModel;
        }
    }
}