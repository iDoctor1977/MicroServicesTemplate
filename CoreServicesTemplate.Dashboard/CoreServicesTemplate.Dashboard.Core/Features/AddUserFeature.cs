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
        private readonly IDefaultMapper<UserAppModel, UserApiModel> _userMapper;

        public AddUserFeature(IStorageRoomService storageRoomService, IDefaultMapper<UserAppModel, UserApiModel> userMapper) 
        {
            _storageRoomService = storageRoomService;
            _userMapper = userMapper;
        }

        public async Task ExecuteAsync(UserAppModel @in)
        {
            var apiModel = _userMapper.Map(@in);

            var responseMessage = await _storageRoomService.AddUserAsync(apiModel);
        }

        public void Execute(UserAppModel model)
        {
            throw new NotImplementedException();
        }
    }
}
