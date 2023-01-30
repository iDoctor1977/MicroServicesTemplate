using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Interfaces;

public interface ISubStepSupplier
{
    Func<UserAppModel, Task<UserAppModel>> AddHandleAsync { get; }
    Func<UserAppModel, Task<UserAppModel>> GetHandleAsync { get; }
}