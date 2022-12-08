using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUserFeature : IGetUserFeature
    {
        private readonly IGetUserDepot _getUserDepot;

        public GetUserFeature(IGetUserDepot getUserDepot) {
            _getUserDepot = getUserDepot;
        }

        public async Task<UserModel> HandleAsync(UserModel model)
        {
            var resultModel = await _getUserDepot.HandleAsync(model);

            return resultModel;
        }
    }
}
