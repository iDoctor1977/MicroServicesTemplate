using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Shared.Core.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.Console.Core.Features
{
    public class ReadUsersFeature : IReadUsersFeature
    {
        private readonly IStorageRoomService _storageRoomService;

        public ReadUsersFeature(IServiceProvider service)
        {
            _storageRoomService = service.GetRequiredService<IStorageRoomService>();
        }

        public async Task<IEnumerable<UserApiModel>> HandleAsync()
        {
            var apiModels = await _storageRoomService.ReadUsersAsync();

            return apiModels;
        }
    }
}