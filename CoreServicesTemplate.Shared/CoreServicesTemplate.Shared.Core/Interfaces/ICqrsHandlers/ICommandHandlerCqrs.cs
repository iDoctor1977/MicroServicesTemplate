using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.ICqrsHandlers;

public interface ICommandHandlerCqrs<in TIn>
{
    public Task HandleAsync(TIn model);
    public void Handle(TIn model);
}