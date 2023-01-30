using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;

public interface IAggregate<T> where T : IAggModel { }