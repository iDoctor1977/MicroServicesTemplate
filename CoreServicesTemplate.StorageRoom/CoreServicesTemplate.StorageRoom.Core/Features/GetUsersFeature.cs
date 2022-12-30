using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUsersFeature : AFeatureQueryBase<UsersModel>
    {
        private readonly IGetUsersDepot _getUsersDepot;

        public GetUsersFeature(IGetUsersDepot getUsersDepot) {
            _getUsersDepot = getUsersDepot;
        }

        public override async Task<UsersModel> HandleAsync()
        {
            var model = await _getUsersDepot.HandleAsync();

            return model;
        }
    }
}
