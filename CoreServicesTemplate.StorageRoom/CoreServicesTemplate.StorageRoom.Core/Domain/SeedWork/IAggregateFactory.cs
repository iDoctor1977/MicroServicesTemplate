namespace CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;

public interface IAggregateFactory
{
    TOut GenerateAggregate<TIn, TOut>(TIn model);
}