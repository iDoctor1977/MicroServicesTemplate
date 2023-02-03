using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Interfaces;

public interface ISubStepSupplier
{
    Func<UserAppModel, UserAppModel> AddHandleAsync { get; }
    Func<UserAppModel, UserAppModel> GetHandleAsync { get; }
}