using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Core.Aggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class AddUserFeature : IAddUserFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IConsolidatorToData<UserModel, UserApiModel> _consolidators;

        public AddUserFeature(IStorageRoomService storageRoomService, IConsolidatorToData<UserModel, UserApiModel> consolidators) 
        {
            _storageRoomService = storageRoomService;
            _consolidators = consolidators;
        }

        public async Task<HttpResponseMessage> HandleAsync(UserModel model)
        {
            var aggregate = new UserAggregate(model);
            aggregate.SetGuid(Guid.NewGuid());

            var apiModel = _consolidators.ToData(aggregate.ToModel()).Resolve();

            var responseMessage = await _storageRoomService.AddUserAsync(apiModel);

            return responseMessage;
        }
    }
}
