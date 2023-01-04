using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles
{
    public interface IFeatureQuery<TOut> where TOut : IAppModel
    {
        public Task<TOut> HandleAsync();
    }

    public interface IFeatureQuery<in TIn, TOut> where TIn : IAppModel where TOut : IAppModel
    {
        public Task<TOut> HandleAsync(TIn @in);
    }

}