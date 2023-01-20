using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandlers
{
    public interface IQueryHandlerFeature<TOut> where TOut : IAppModel
    {
        public Task<TOut> HandleAsync();
        public TOut Handle();
    }

    public interface IQueryHandlerFeature<in TIn, TOut> where TIn : IAppModel where TOut : class
    {
        public Task<TOut> HandleAsync(TIn @in);
        public TOut Handle(TIn @in);
    }
}