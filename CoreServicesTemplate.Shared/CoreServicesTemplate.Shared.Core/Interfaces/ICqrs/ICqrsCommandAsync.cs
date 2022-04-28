using System.Threading.Tasks;

namespace CoreServicesTemplate.Shared.Core.Interfaces.ICqrs
{
    public interface ICqrsCommandAsync<in T> where T : ICqrsCommandBase
    {
        public Task ExecuteAsync(T model);
    }
}