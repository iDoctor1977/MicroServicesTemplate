using CoreServicesTemplate.Shared.Core.Enums;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

public interface ICommandHandler<in TIn> where TIn : class
{
    public Task<OperationResult> ExecuteAsync(TIn model);
}