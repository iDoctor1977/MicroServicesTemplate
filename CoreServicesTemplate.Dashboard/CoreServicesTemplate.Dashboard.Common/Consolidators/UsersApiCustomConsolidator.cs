using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Common.Consolidators
{
    public class UsersApiCustomConsolidator : ACustomConsolidatorBase<UsersApiModel, UsersModel>
    {
        private readonly IConsolidatorToData<UserApiModel, UserModel> _userConsolidator;

        public UsersApiCustomConsolidator(ICustomMapper customMapper, IConsolidatorToData<UserApiModel, UserModel> userConsolidator) : base(customMapper)
        {
            _userConsolidator = userConsolidator;

            ModelIn.UsersApiModelList = new List<UserApiModel>();
            ModelOut.UsersModelList = new List<UserModel>();

        }

        public override IConsolidatorToResolve<UsersApiModel, UsersModel> ToData(UsersApiModel @in)
        {
            ModelIn = @in;
            ModelOut = InDataToOutData(@in);

            var modelList = new List<UserModel>();
            foreach (var modelIn in @in.UsersApiModelList)
            {
                modelList.Add(_userConsolidator.ToData(modelIn).Resolve());
            }

            ModelOut.UsersModelList = modelList;

            return this;
        }

        public override IConsolidatorToResolveReversing<UsersApiModel, UsersModel> ToDataReverse(UsersModel @out)
        {
            ModelIn = OutDataToInData(ModelOut);

            var modelList = new List<UserApiModel>();
            foreach (var userModel in @out.UsersModelList)
            {
                modelList.Add(_userConsolidator.ToDataReverse(userModel).Resolve());
            }

            ModelIn.UsersApiModelList = modelList;

            return this;
        }
    }
}