namespace CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;

public interface IDomainEntityFactory
{
    TOut GenerateAggregate<TIn, TOut>(TIn model);
}