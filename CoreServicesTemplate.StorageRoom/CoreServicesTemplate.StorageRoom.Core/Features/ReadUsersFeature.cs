using System;
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

        public async Task<UsersApiModel> HandleAsync()
        {
            var model = await _readUsersDepot.HandleAsync();

            return model;
        }
    }
}
