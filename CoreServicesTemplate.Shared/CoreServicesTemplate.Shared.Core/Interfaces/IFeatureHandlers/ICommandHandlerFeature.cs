using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandlers;

public interface ICommandHandlerFeature<in TIn> where TIn : IAppModel
{
    public Task HandleAsync(TIn @in);
}