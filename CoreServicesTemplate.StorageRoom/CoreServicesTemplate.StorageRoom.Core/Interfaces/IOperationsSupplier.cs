using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Interfaces;

public interface IOperationsSupplier
{
    #region OPERATIONS

    Func<UserAggregate, Task<UserAggregate>> HandleAddAsync { get; }
    Func<UserAggregate, Task<UserAggregate>> HandleGetAsync { get; }

    #endregion

    #region FUNCTIONS

    Func<UserAggregate, Task<UserAggregate>> CalculateGuidAsync { get; }

    #endregion
}

public interface IAddUserPipe : ICommandHandler<UserAggregate> { }