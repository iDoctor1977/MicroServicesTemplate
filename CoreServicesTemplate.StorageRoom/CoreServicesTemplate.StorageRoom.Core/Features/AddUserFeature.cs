using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class AddUserFeature : AFeatureCommandBase<UserAggregate, UserModel>
    {
        private readonly IAddUserDepot _addUserDepot;
        private readonly IOperationsSupplier _operationsSupplier;

        public AddUserFeature(IAddUserDepot addUserDepot, IOperationsSupplier operationsSupplier)
        {
            _addUserDepot = addUserDepot;
            _operationsSupplier = operationsSupplier;
        }

        public override async Task HandleAsync()
        {
            // Attach model to your model domain logic
            await _operationsSupplier.CalculateGuidAsync(Aggregate);

            // execute interaction with repository if necessary
            await _addUserDepot.HandleAsync(Aggregate.ToModel());

            // execute addUserFeature sub steps
            // this part is added only for features scalability 
            await _operationsSupplier.HandleAddAsync(Aggregate);
        }

        public override ICommandHandleAggregate SetAggregate(UserModel model)
        {
            Aggregate = new UserAggregate(model);

            return this;
        }
    }
}
