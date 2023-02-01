using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

public interface ICommandHandler<in TIn> where TIn : class
{
    public Task HandleAsync(TIn model);
    public void Handle(TIn model);
}