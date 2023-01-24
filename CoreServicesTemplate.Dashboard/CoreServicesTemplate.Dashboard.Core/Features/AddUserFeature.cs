using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class AddUserFeature : IAddUserFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IConsolidator<UserAppModel, UserApiModel> _consolidators;

        public AddUserFeature(IStorageRoomService storageRoomService, IConsolidator<UserAppModel, UserApiModel> consolidators) 
        {
            _storageRoomService = storageRoomService;
            _consolidators = consolidators;
        }

        public async Task HandleAsync(UserAppModel @in)
        {
            var apiModel = _consolidators.ToData(@in).Resolve();

            var responseMessage = await _storageRoomService.AddUserAsync(apiModel);
        }
    }
}
