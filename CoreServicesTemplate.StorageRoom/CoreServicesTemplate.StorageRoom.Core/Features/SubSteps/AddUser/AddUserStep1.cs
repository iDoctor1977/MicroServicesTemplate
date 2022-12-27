using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser
{
    [Root]
    public class AddUserStep1 : RootPipelineBuilder<AddAggregate, AddAggregate>
    {
        private readonly IAddUserDepot _addUserDepot;

        public AddUserStep1(IAddUserDepot addUserDepot)
        {
            _addUserDepot = addUserDepot;
        }

        protected override AddAggregate ExecuteRootStep(AddAggregate aggregate)
        {
            // Read
            var model = aggregate.ToModel();

            // Do
            _addUserDepot.HandleAsync(model);

            // Write

            return aggregate;
        }
    }
}