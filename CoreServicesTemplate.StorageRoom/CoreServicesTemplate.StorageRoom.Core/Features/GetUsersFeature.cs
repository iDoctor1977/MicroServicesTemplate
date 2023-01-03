using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUsersFeature : AFeatureQueryBase<UsersAppModel>
    {
        private readonly IGetUsersDepot _getUsersDepot;

        public GetUsersFeature(IGetUsersDepot getUsersDepot) {
            _getUsersDepot = getUsersDepot;
        }

        public override async Task<UsersAppModel> HandleAsync()
        {
            var model = await _getUsersDepot.HandleAsync();

            return model;
        }
    }
}
