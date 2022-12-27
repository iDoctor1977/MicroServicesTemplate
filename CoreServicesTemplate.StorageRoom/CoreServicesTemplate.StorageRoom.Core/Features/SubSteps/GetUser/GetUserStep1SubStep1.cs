using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser
{
    [Leaf(nameof(GetUserStep1))]
    public class GetUserStep1SubStep1 : ISubStep<UserAggregate, UserAggregate>
    {
        public GetUserStep1SubStep1(IServiceProvider service) { }

        public async Task<UserAggregate> ExecuteAsync(UserAggregate aggregate)
        {
            aggregate.ToModel();

            return aggregate;
        }
    }
}