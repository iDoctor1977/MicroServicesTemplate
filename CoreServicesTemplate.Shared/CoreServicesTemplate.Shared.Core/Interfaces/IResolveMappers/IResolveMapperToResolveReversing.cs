namespace CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;

public interface IResolveMapperToResolveReversing<out TIn, TOut>
{
    TIn Resolve();
}