using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Interfaces;

public interface ISubStepSupplier
{
    Func<UserAppModel, OperationResult<UserAppModel>> ExecuteAddAsync { get; }
    Func<UserAppModel, OperationResult<UserAppModel>> ExecuteGetAsync { get; }
}