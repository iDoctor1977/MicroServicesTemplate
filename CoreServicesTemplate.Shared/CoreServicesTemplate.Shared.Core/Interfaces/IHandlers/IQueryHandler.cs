using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Enums;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

public interface IQueryHandler<in TIn, TOut> where TIn : class where TOut : class
{
    Task<OperationResult<TOut>> ExecuteAsync(TIn model);
}

public interface IQueryHandler<TOut>
{
    Task<OperationResult<TOut>> ExecuteAsync();
}