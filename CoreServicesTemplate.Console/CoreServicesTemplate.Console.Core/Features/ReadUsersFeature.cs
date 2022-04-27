using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreServicesTemplate.Console.Core.Features
{
    public class ReadUsersFeature : IReadUsersFeature
    {
        private readonly IStorageRoomService _storageRoomService;

        public ReadUsersFeature(IServiceProvider service)
        {
            _storageRoomService = service.GetRequiredService<IStorageRoomService>();
        }

        public async Task<IEnumerable<UserApiModel>> ExecuteAsync(object model)
        {
            var apiModels = await _storageRoomService.ReadUsersAsync();

            return apiModels;
        }
    }
}