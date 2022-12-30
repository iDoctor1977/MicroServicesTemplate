using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class AFeatureCommandBase<TA, TIn> : IFeatureCommand<TIn>, ICommandHandleAggregate where TA : IAggregate<TIn> where TIn : IAppModel
{
    protected TA Aggregate { get; set; }

    public abstract Task HandleAsync();
    public abstract ICommandHandleAggregate SetAggregate(TIn model);
}

public abstract class AFeatureCommandBase<T1A, T2A, T1In, T2In> : IFeatureCommand<T1In, T2In>, ICommandHandleAggregate
    where T1A : IAggregate<T1In>
    where T2A : IAggregate<T2In>
    where T1In : IAppModel
    where T2In : IAppModel
{
    protected T1A Aggregate1 { get; set; }
    protected T2A Aggregate2 { get; set; }

    public abstract Task HandleAsync();
    public abstract ICommandHandleAggregate SetAggregate(T1In model1, T2In model2);
}