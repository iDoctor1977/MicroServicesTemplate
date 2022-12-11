using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.Receivers
{
    public class GetUsersCoreCustomReceiver : AConsolidatorBase<UsersApiModel, UsersModel>
    {
        private readonly IConsolidators<UserApiModel, UserModel> _customReceiver;

        public GetUsersCoreCustomReceiver(ICustomMapper customMapper, IConsolidators<UserApiModel, UserModel> customReceiver) : base(customMapper)
        {
            _customReceiver = customReceiver;
        }

        public override UsersModel ToData(UsersApiModel apiModel)
        {
            var model = ToExternalData(apiModel);
            model.UsersModelList = _customReceiver.ToData(apiModel.UsersApiModelList);

            return model;
        }
    }
}