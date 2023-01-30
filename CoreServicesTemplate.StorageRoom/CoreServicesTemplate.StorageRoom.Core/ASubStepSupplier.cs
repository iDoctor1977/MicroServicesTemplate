using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser;
using CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser;

namespace CoreServicesTemplate.StorageRoom.Core
{
    public abstract class ASubStepSupplier : ISubStepSupplier
    {
        protected readonly AddUserStep1 AddUserStep1;
        protected readonly AddUserStep1SubStep1 AddUserStep1SubStep1;
        protected readonly AddUserStep1SubStep2 AddUserStep1SubStep2;

        protected readonly GetUserStep1 GetUserStep1;
        protected readonly GetUserStep1SubStep1 GetUserStep1SubStep1;

        protected ASubStepSupplier(
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

        public Func<UserAppModel, Task<UserAppModel>> AddHandleAsync => PipeAddDefinitionAsync;
        protected abstract Task<UserAppModel> PipeAddDefinitionAsync(UserAppModel aggregate);

        public Func<UserAppModel, Task<UserAppModel>> GetHandleAsync => PipeGetDefinitionAsync;
        protected abstract Task<UserAppModel> PipeGetDefinitionAsync(UserAppModel aggregate);
    }
}
