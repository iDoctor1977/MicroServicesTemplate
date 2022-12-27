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

        protected override Task<AddAggregate> PipeAdd(AddAggregate aggregate)
        {
            aggregate = AddUserStep1.AddSubStep(AddUserStep1SubStep1).AddSubStep(AddUserStep1SubStep2).Execute(aggregate);

            return Task.FromResult(aggregate);
        }

        protected override Task<GetAggregate> PipeGet(GetAggregate aggregate)
        {
            aggregate = GetUserStep1.AddSubStep(GetUserStep1SubStep1).Execute(aggregate);

            return Task.FromResult(aggregate);
        }

        #endregion

        #region FUNCTIONS

        protected override Task<AddAggregate> FuncCalculateGuid(AddAggregate aggregate)
        {
            var a = Guid.NewGuid();

            aggregate.SetGuid(a);

            return Task.FromResult(aggregate);
        }

        #endregion
    }
}
