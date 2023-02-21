using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Interfaces;

public interface ISubStepSupplier
{
    Func<UserAppModel, UserAppModel> ExecuteAddAsync { get; }
    Func<UserAppModel, UserAppModel> ExecuteGetAsync { get; }
}