using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Interfaces;

public interface IOperationsSupplier
{
    #region OPERATIONS

    Func<AddAggregate, Task<AddAggregate>> HandleAddAsync { get; }
    Func<GetAggregate, Task<GetAggregate>> HandleGetAsync { get; }

    #endregion

    #region FUNCTIONS

    Func<AddAggregate, Task<AddAggregate>> CalculateGuid { get; }

    #endregion
}

public interface IAddUserPipe : ICommandHandler<AddAggregate> { }