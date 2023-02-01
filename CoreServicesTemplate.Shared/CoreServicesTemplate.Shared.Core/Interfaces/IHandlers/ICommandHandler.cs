using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

public interface ICommandHandler<in TIn> where TIn : class
{
    public Task ExecuteAsync(TIn model);
    public void Execute(TIn model);
}