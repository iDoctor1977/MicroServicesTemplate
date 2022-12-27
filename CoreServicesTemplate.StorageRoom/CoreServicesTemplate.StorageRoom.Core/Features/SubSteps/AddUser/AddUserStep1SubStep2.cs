using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser
{
    [Leaf(nameof(AddUserStep1))]
    public class AddUserStep1SubStep2 : ISubStep<UserAggregate, UserAggregate>
    {
        public AddUserStep1SubStep2(IServiceProvider service) { }

        public async Task<UserAggregate> ExecuteAsync(UserAggregate aggregate)
        {
            aggregate.ToModel();

            return aggregate;
        }
    }
}