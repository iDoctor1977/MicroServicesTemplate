using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandlers;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class GetUsersFeature : IQueryHandlerFeature<UsersAppModel>
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IConsolidator<UsersApiModel, UsersAppModel> _consolidators;

        public GetUsersFeature(IStorageRoomService storageRoomService, IConsolidator<UsersApiModel, UsersAppModel> consolidators)
        {
            _storageRoomService = storageRoomService;
            _consolidators = consolidators;
        }

        public async Task<UsersAppModel> HandleAsync()
        {
            var apiModel = await _storageRoomService.GetUsersAsync();

            var model = _consolidators.ToData(apiModel).Resolve();

            return model;
        }

        public UsersAppModel Handle()
        {
            var apiModel = _storageRoomService.GetUsers();

            var model = _consolidators.ToData(apiModel).Resolve();

            return model;
        }
    }
}