using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.ICqrs
{
    public interface IQueryHandlerCqrs<in TIn, TOut>
    {
        Task<TOut> HandleAsync(TIn model);
        TOut Handle(TIn model);
    }

    public interface IQueryHandlerCqrs<TOut>
    {
        Task<TOut> HandleAsync();
        TOut Handle();
    }
}