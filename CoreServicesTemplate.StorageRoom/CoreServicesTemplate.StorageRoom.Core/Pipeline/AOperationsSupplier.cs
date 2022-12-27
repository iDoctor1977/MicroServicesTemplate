using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using System;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser;

namespace CoreServicesTemplate.StorageRoom.Core.Pipeline
{
    public abstract class AOperationsSupplier : IOperationsSupplier
    {
        protected readonly AddUserStep1 AddUserStep1;
        protected readonly AddUserStep1SubStep1 AddUserStep1SubStep1;
        protected readonly AddUserStep1SubStep2 AddUserStep1SubStep2;

        protected readonly GetUserStep1 GetUserStep1;
        protected readonly GetUserStep1SubStep1 GetUserStep1SubStep1;

        protected AOperationsSupplier(
            AddUserStep1 addUserStep1,
            AddUserStep1SubStep1 addUserStep1SubStep1,
            AddUserStep1SubStep2 addUserStep1SubStep2,
            GetUserStep1 getUserStep1, 
            GetUserStep1SubStep1 getUserStep1SubStep1)
        {
            AddUserStep1 = addUserStep1;
            AddUserStep1SubStep1 = addUserStep1SubStep1;
            AddUserStep1SubStep2 = addUserStep1SubStep2;

            GetUserStep1 = getUserStep1;
            GetUserStep1SubStep1 = getUserStep1SubStep1;
        }

        #region PIPELINE PROCEDURES

        public Func<AddAggregate, AddAggregate> ExecuteAddPipeline => PipeAdd;
        protected abstract AddAggregate PipeAdd(AddAggregate aggregate);

        public Func<GetAggregate, GetAggregate> ExecuteGetPipeline => PipeGet;
        protected abstract GetAggregate PipeGet(GetAggregate aggregate);

        #endregion

        #region FUNCTIONS

        public Func<AddAggregate, AddAggregate> CalculateGuid => FuncCalculateGuid;
        protected abstract AddAggregate FuncCalculateGuid(AddAggregate aggregate);

        #endregion
    }
}
