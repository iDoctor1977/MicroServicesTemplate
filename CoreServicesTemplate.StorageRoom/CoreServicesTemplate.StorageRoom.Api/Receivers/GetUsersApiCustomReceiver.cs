using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.ICustomMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Api.Receivers
{
    public class GetUsersApiCustomReceiver : AConsolidatorBase<UsersApiModel, UsersModel>
    {
        private readonly IConsolidators<UserApiModel, UserModel> _customReceiver;

        public GetUsersApiCustomReceiver(ICustomMapper customMapper, IConsolidators<UserApiModel, UserModel> customReceiver) : base(customMapper)
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