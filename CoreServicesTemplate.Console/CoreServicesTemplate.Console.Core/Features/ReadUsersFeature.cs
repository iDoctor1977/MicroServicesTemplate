using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Core.Features
{
    public class ReadUsersFeature : IReadUsersFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IConsolidators<UsersApiModel, UsersModel> _consolidators;

        public ReadUsersFeature(IStorageRoomService storageRoomService, IConsolidators<UsersApiModel, UsersModel> consolidators)
        {
            _storageRoomService = storageRoomService;
            _consolidators = consolidators;
        }

        public async Task<UsersModel> HandleAsync()
        {
            var apiModel = await _storageRoomService.ReadUsersAsync();

            var model = _consolidators.ToData(apiModel);

            return model;
        }
    }
}