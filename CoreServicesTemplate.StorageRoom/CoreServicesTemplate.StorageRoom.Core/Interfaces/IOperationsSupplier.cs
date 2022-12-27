using System;
using CoreServicesTemplate.StorageRoom.Core.Aggregates;

namespace CoreServicesTemplate.StorageRoom.Core.Interfaces;

public interface IOperationsSupplier
{
    #region OPERATIONS

    Func<AddAggregate, AddAggregate> ExecuteAddPipeline { get; }
    Func<GetAggregate, GetAggregate> ExecuteGetPipeline { get; }

    #endregion

    #region FUNCTIONS

    Func<AddAggregate, AddAggregate> CalculateGuid { get; }

    #endregion
}