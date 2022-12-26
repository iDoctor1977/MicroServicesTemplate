using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class GetUsersFeature : IGetUsersFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IConsolidatorToData<UsersApiModel, UsersModel> _consolidators;

        public GetUsersFeature(IStorageRoomService storageRoomService, IConsolidatorToData<UsersApiModel, UsersModel> consolidators)
        {
            _storageRoomService = storageRoomService;
            _consolidators = consolidators;
        }

        public async Task<UsersModel> HandleAsync()
        {
            var apiModel = await _storageRoomService.GetUsersAsync();

            var model = _consolidators.ToData(apiModel).Resolve();

            return model;
        }
    }
}