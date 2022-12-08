using System;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class AddUserFeature : IAddUserFeature
    {
        private readonly IAddUserDepot _addUserDepot;

        public AddUserFeature(IAddUserDepot addUserDepot) {
            _addUserDepot = addUserDepot;
        }

        public async Task HandleAsync(UserModel model)
        {
            var aggregate = new CreateAggregate(model);
            aggregate.SetGuid(Guid.NewGuid());

            await _addUserDepot.HandleAsync(aggregate.ToModel());
        }
    }
}
