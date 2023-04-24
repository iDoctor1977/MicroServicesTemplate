namespace CoreServicesTemplate.Shared.Core.Interfaces.IFactories;

public interface IDomainEntityFactory
{
    TOut GenerateAggregate<TIn, TOut>(TIn model);
}