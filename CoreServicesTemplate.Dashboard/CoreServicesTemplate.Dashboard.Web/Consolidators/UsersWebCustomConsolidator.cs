using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.Dashboard.Web.Consolidators
{
    public class UsersWebCustomConsolidator : ACustomConsolidatorBase<UsersViewModel, UsersAppModel>
    {
        private readonly IConsolidator<UserViewModel, UserAppModel> _userConsolidator;

        public UsersWebCustomConsolidator(ICustomMapper customMapper, IConsolidator<UserViewModel, UserAppModel> userConsolidator) : base(customMapper)
        {
            _userConsolidator = userConsolidator;

            ModelIn.UsersViewModelList = new List<UserViewModel>();
            ModelOut.UsersModelList = new List<UserAppModel>();

        }

        public override IConsolidatorToResolve<UsersViewModel, UsersAppModel> ToData(UsersViewModel @in)
        {
            ModelIn = @in;
            ModelOut = InDataToOutData(@in);

            var modelList = new List<UserAppModel>();
            foreach (var modelIn in @in.UsersViewModelList)
            {
                modelList.Add(_userConsolidator.ToData(modelIn).Resolve());
            }

            ModelOut.UsersModelList = modelList;

            return this;
        }

        public override IConsolidatorToResolveReversing<UsersViewModel, UsersAppModel> ToDataReverse(UsersAppModel @out)
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