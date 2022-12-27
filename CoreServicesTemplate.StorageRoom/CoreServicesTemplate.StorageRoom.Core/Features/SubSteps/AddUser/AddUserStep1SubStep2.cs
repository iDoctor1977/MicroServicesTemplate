using System;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser
{
    [Leaf(nameof(AddUserStep1))]
    public class AddUserStep1SubStep2 : ISubStep<AddAggregate, AddAggregate>
    {
        public AddUserStep1SubStep2(IServiceProvider service) { }

        public AddAggregate Execute(AddAggregate aggregate)
        {
            // Read

            // Do

            // Write
            aggregate.ToModel();

            return aggregate;
        }
    }
}