namespace CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;

public interface IResolveMapperToResolve<TIn, out TOut>
{
    TOut Resolve();
}