using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class AFeatureQueryBase<TIn, TOut> : IFeatureQuery<TIn, TOut>, IQueryHandleAggregate<TOut>
    where TIn : IAppModel
    where TOut : IAppModel
{
    protected TIn ModelAppIn { get; set; }
    protected TOut ModelAppOut { get; set; }

    public abstract IQueryHandleAggregate<TOut> SetModel(TIn model);
    public abstract Task<TOut> HandleAsync();
}

public abstract class AFeatureQueryBase<TOut> : IFeatureQuery<TOut> where TOut : IAppModel
{
    public abstract Task<TOut> HandleAsync();
}