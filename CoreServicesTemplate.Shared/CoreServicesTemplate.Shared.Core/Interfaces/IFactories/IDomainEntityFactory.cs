namespace CoreServicesTemplate.Shared.Core.Interfaces.IFactories;

public interface IDomainEntityFactory
{
    TOut Generate<TIn, TOut>(TIn model);
}