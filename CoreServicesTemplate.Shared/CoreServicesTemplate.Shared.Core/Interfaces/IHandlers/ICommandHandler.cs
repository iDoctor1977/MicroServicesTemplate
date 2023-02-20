using CoreServicesTemplate.Shared.Core.Enums;
using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

public interface ICommandHandler<in TIn> where TIn : class
{
    public Task<OperationResult> ExecuteAsync(TIn model);
}