namespace CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;

public interface IDomainFactory
{
    TOut GenerateAggregate<TIn, TOut>(TIn model);
}