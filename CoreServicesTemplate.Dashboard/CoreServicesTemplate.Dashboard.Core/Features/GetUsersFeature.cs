using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class GetUsersFeature : IGetUsersFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IMapperService<UsersApiModel, UsersAppModel> _mapper;

        public GetUsersFeature(IStorageRoomService storageRoomService, IMapperService<UsersApiModel, UsersAppModel> mapper)
        {
            _storageRoomService = storageRoomService;
            _mapper = mapper;
        }

        public async Task<UsersAppModel> ExecuteAsync()
        {
            var apiModel = await _storageRoomService.GetUsersAsync();

            var model = _mapper.Map(apiModel);

            return model;
        }

        public UsersAppModel Execute()
        {
            var apiModel = _storageRoomService.GetUsers();

            var model = _mapper.Map(apiModel);

            return model;
        }
    }
}