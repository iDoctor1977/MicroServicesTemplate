namespace CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;

public interface IConsolidatorToResolve<TIn, out TOut>
{
    TOut Resolve();
}