using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class AFeatureQueryBase<TA, TIn, TOut> : IFeatureQuery<TIn, TOut>, IQueryHandleAggregate<TOut>
    where TA : IAggregate<TIn>
    where TIn : IAppModel
    where TOut : IAppModel
{
    protected TA AggregateModel { get; set; }

    public abstract IQueryHandleAggregate<TOut> SetAggregate(TIn model);
    public abstract Task<TOut> HandleAsync();
}

public abstract class AFeatureQueryBase<T1A, T2A, T1In, T2In, TOut> : IFeatureQuery<T1In, T2In, TOut>, IQueryHandleAggregate<TOut>
    where T1A : IAggregate<T1In>
    where T2A : IAggregate<T2In>
    where T1In : IAppModel
    where T2In : IAppModel
    where TOut : IAppModel
{
    protected T1A AggregateModel1 { get; set; }
    protected T2A AggregateModel2 { get; set; }

    public abstract IQueryHandleAggregate<TOut> SetAggregate(T1In model1, T2In model2);
    public abstract Task<TOut> HandleAsync();
}

public abstract class AFeaturesQueryBase<TOut> : IFeatureQuery<TOut> where TOut : IAppModel
{
    public abstract Task<TOut> HandleAsync();
}