using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class ReadUsersFeature : IReadUsersFeature
    {
        private readonly IReadUsersDepot _readUsersDepot;

        public ReadUsersFeature(IReadUsersDepot readUsersDepot) {
            _readUsersDepot = readUsersDepot;
        }

        public async Task<UsersApiModel> HandleAsync()
        {
            var model = await _readUsersDepot.HandleAsync();

            return model;
        }
    }
}
