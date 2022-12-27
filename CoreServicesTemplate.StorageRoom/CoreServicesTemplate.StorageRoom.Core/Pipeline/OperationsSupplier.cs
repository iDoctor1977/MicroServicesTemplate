using System;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser;

namespace CoreServicesTemplate.StorageRoom.Core.Pipeline
{
    public sealed class OperationsSupplier : AOperationsSupplier
    {
        public OperationsSupplier(
                AddUserStep1 addUserStep1,
                AddUserStep1SubStep1 addUserStep1SubStep1,
                AddUserStep1SubStep2 addUserStep1SubStep2,
                GetUserStep1 getUserStep1, 
                GetUserStep1SubStep1 getUserStep1SubStep1) : base(addUserStep1, addUserStep1SubStep1, addUserStep1SubStep2, getUserStep1, getUserStep1SubStep1) { }

        #region PIPELINE PROCEDURES

        protected override async Task<UserAggregate> PipeAddDefinitionAsync(UserAggregate aggregate)
        {
            aggregate = await AddUserStep1.AddSubStep(AddUserStep1SubStep1).AddSubStep(AddUserStep1SubStep2).ExecuteAsync(aggregate);

            return aggregate;
        }

        protected override async Task<UserAggregate> PipeGetDefinitionAsync(UserAggregate aggregate)
        {
            aggregate = await GetUserStep1.AddSubStep(GetUserStep1SubStep1).ExecuteAsync(aggregate);

            return aggregate;
        }

        #endregion

        #region FUNCTIONS

        protected override Task<UserAggregate> FuncCalculateGuidAsync(UserAggregate aggregate)
        {
            var a = Guid.NewGuid();

            aggregate.SetGuid(a);

            return Task.FromResult(aggregate);
        }

        #endregion
    }
}
