using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;

public interface IFeatureCommand<in TIn> where TIn : IAppModel
{
    public Task HandleAsync(TIn @in);
}