using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Core.Aggregates;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class AddUserFeature : AFeatureCommandBase<UserAggregate, UserModel>
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IConsolidator<UserModel, UserApiModel> _consolidators;

        public AddUserFeature(IStorageRoomService storageRoomService, IConsolidator<UserModel, UserApiModel> consolidators) 
        {
            _storageRoomService = storageRoomService;
            _consolidators = consolidators;
        }

        public override ICommandHandleAggregate SetAggregate(UserModel model)
        {
            Aggregate = new UserAggregate(model);

            return this;
        }

        public override async Task HandleAsync()
        {
            Aggregate.SetGuid(Guid.NewGuid());

            var apiModel = _consolidators.ToData(Aggregate.ToModel()).Resolve();

            var responseMessage = await _storageRoomService.AddUserAsync(apiModel);
        }
    }
}
