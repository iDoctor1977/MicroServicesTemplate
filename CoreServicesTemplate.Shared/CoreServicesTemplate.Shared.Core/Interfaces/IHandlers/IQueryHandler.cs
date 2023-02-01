using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

public interface IQueryHandler<in TIn, TOut> where TIn : class where TOut : class
{
    Task<TOut> HandleAsync(TIn model);
    TOut Handle(TIn model);
}

public interface IQueryHandler<TOut>
{
    Task<TOut> HandleAsync();
    TOut Handle();
}