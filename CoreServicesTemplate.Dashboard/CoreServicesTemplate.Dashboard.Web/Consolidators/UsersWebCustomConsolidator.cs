using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;

namespace CoreServicesTemplate.Dashboard.Web.Consolidators
{
    public class UsersWebCustomConsolidator : ACustomConsolidatorBase<UsersViewModel, UsersModel>
    {
        private readonly IConsolidator<UserViewModel, UserModel> _userConsolidator;

        public UsersWebCustomConsolidator(ICustomMapper customMapper, IConsolidator<UserViewModel, UserModel> userConsolidator) : base(customMapper)
        {
            _userConsolidator = userConsolidator;

            ModelIn.UsersViewModelList = new List<UserViewModel>();
            ModelOut.UsersModelList = new List<UserModel>();

        }

        public override IConsolidatorToResolve<UsersViewModel, UsersModel> ToData(UsersViewModel @in)
        {
            ModelIn = @in;
            ModelOut = InDataToOutData(@in);

            var modelList = new List<UserModel>();
            foreach (var modelIn in @in.UsersViewModelList)
            {
                modelList.Add(_userConsolidator.ToData(modelIn).Resolve());
            }

            ModelOut.UsersModelList = modelList;

            return this;
        }

        public override IConsolidatorToResolveReversing<UsersViewModel, UsersModel> ToDataReverse(UsersModel @out)
        {
            ModelIn = OutDataToInData(@out);

            var modelList = new List<UserViewModel>();
            foreach (var userModel in @out.UsersModelList)
            {
                modelList.Add(_userConsolidator.ToDataReverse(userModel).Resolve());
            }

            ModelIn.UsersViewModelList = modelList;

            return this;
        }
    }
}