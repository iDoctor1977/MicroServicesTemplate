using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles
{
    public interface IQueryHandler<in TIn, TOut>
    {
        Task<TOut> HandleAsync(TIn model);
    }

    public interface IQueryHandler<TOut>
    {
        Task<TOut> HandleAsync();
    }
}