using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.ICqrs
{
    public interface ICqrsQueryAsync<in TIn, TOut> where TIn : ICqrsQueryBase
    {
        Task<TOut> ExecuteAsync(TIn model);
    }
}