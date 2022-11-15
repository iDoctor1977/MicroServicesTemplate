using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class CreateUserFeature : ICreateUserFeature
    {
        private readonly ICreateUserDepot _createUserDepot;

        public CreateUserFeature(ICreateUserDepot createUserDepot) {
            _createUserDepot = createUserDepot;
        }

        public async Task HandleAsync(UserApiModel model)
        {
            var aggregate = new CreateAggregate(model);
            aggregate.SetGuid(Guid.NewGuid());

            await _createUserDepot.HandleAsync(aggregate.ToModel());
        }
    }
}
