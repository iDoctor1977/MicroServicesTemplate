namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.SeedWork;

public interface IAggregateFactory
{
    TOut GenerateAggregate<TIn, TOut>(TIn model);
}