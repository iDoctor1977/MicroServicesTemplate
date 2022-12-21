namespace CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;

public interface IConsolidatorToResolveReversing<out TIn, TOut>
{
    TIn Resolve();
}