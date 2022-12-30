using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser
{
    [Root]
    public class AddUserStep1 : RootPipelineBuilder<UserAggregate, UserAggregate>
    {
        private readonly IAddUserDepot _addUserDepot;

        public AddUserStep1(IAddUserDepot addUserDepot)
        {
            _addUserDepot = addUserDepot;
        }

        protected override async Task<UserAggregate> HandleRootStepAsync(UserAggregate aggregate)
        {
            // Do anything on Depot
            // Example: aggregate.SetName("Alfred");
            //          await _addUserDepot.HandleAsync(aggregate.ToModel());

            return aggregate;
        }
    }
}