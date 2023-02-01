using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class AddUserFeature : IAddUserFeature
    {
        private readonly IStorageRoomService _storageRoomService;
        private readonly IMapperService<UserAppModel, UserApiModel> _mapper;

        public AddUserFeature(IStorageRoomService storageRoomService, IMapperService<UserAppModel, UserApiModel> mapper) 
        {
            _storageRoomService = storageRoomService;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(UserAppModel @in)
        {
            var apiModel = _mapper.Map(@in);

            var responseMessage = await _storageRoomService.AddUserAsync(apiModel);
        }

        public void Execute(UserAppModel model)
        {
            throw new NotImplementedException();
        }
    }
}
