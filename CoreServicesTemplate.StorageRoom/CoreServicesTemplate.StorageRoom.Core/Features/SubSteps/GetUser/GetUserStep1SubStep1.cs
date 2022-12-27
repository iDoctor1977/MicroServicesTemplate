using System;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser
{
    [Leaf(nameof(GetUserStep1))]
    public class GetUserStep1SubStep1 : ISubStep<GetAggregate, GetAggregate>
    {
        public GetUserStep1SubStep1(IServiceProvider service) { }

        public GetAggregate Execute(GetAggregate aggregate)
        {
            // Read

            // Do

            // Write
            aggregate.ToModel();

            return aggregate;
        }
    }
}