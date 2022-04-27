using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class ReadUsersFeature : IReadUsersFeature
    {
        private readonly IReadUsersDepot _readUsersDepot;

        public ReadUsersFeature(IServiceProvider service) {
            _readUsersDepot = service.GetRequiredService<IReadUsersDepot>();
        }

        public async Task<IEnumerable<UserApiModel>> ExecuteAsync()
        {
            var users = await _readUsersDepot.ExecuteAsync(null);

            return users;
        }
    }
}
