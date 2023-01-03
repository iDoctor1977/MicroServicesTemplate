using System;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Common.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Interfaces;

public interface ISubStepSupplier
{
    Func<UserAppModel, Task<UserAppModel>> HandleAddAsync { get; }
    Func<UserAppModel, Task<UserAppModel>> HandleGetAsync { get; }
}