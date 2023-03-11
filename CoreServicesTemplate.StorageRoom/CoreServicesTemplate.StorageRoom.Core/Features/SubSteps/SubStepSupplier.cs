using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps
{
    public sealed class SubStepSupplier : ASubStepSupplier
    {
        public SubStepSupplier(
                AddUserStep1 addUserStep1,
                AddUserStep1SubStep1 addUserStep1SubStep1,
                AddUserStep1SubStep2 addUserStep1SubStep2,
                GetUserStep1 getUserStep1,
                GetUserStep1SubStep1 getUserStep1SubStep1) : base(addUserStep1, addUserStep1SubStep1, addUserStep1SubStep2, getUserStep1, getUserStep1SubStep1) { }

        protected override UserAppModel PipeAddDefinitionAsync(UserAppModel aggregate)
        {
            aggregate = AddUserStep1.AddSubStep(AddUserStep1SubStep1).AddSubStep(AddUserStep1SubStep2).ExecuteAsync(aggregate);

            return aggregate;
        }

        protected override UserAppModel PipeGetDefinitionAsync(UserAppModel aggregate)
        {
            aggregate = GetUserStep1.AddSubStep(GetUserStep1SubStep1).ExecuteAsync(aggregate);

            return aggregate;
        }
    }
}
