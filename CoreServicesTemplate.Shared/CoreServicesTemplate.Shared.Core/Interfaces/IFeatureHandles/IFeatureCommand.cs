using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;

public interface IFeatureCommand<in TIn> where TIn : IAppModel
{
    public abstract Task HandleAsync(TIn @in);
}