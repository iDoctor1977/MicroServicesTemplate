using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles
{
    public interface ICommandHandler<in T>
    {
        public Task HandleAsync(T model);
    }
}