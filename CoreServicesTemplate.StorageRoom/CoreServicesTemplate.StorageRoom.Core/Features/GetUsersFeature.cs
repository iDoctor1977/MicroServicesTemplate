using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUsersFeature : IGetUsersFeature
    {
        private readonly IGetUsersDepot _getUsersDepot;

        public GetUsersFeature(IGetUsersDepot getUsersDepot) {
            _getUsersDepot = getUsersDepot;
        }

        public async Task<UsersAppModel> HandleAsync()
        {
            var model = await _getUsersDepot.HandleAsync();

            return model;
        }

        public UsersAppModel Handle()
        {
            throw new System.NotImplementedException();
        }
    }
}
