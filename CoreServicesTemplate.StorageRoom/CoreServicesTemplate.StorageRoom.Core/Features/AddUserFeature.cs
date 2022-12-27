using System;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class AddUserFeature : IAddUserFeature
    {
        private readonly IAddUserDepot _addUserDepot;
        private readonly IOperationsSupplier _operationsSupplier;

        public AddUserFeature(IAddUserDepot addUserDepot, IOperationsSupplier operationsSupplier)
        {
            _addUserDepot = addUserDepot;
            _operationsSupplier = operationsSupplier;
        }

        public async Task HandleAsync(UserModel model)
        {
            // Attach model to your model domain logic
            var aggregate = new AddAggregate(model);
            aggregate.SetGuid(Guid.NewGuid());

            // execute interaction with repository if necessary
            await _addUserDepot.HandleAsync(aggregate.ToModel());

            // execute addUserFeature sub steps
            // this part is added only for features scalability 
            _operationsSupplier.ExecuteAddPipeline(aggregate);
        }
    }
}
