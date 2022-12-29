using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles
{
    // Feature Command

    public interface ICommandHandler<in TIn>  where TIn : IAppModel
    {
        ICommandHandleAggregate SetAggregate(TIn model);
    }

    public interface ICommandHandleAggregate
    {
        public Task HandleAsync();
    }

    public interface IFeatureCommand<in TIn> : ICommandHandler<TIn> where TIn : IAppModel { }

    public abstract class AFeatureCommandBase<TA, TIn> : IFeatureCommand<TIn>, ICommandHandleAggregate where TA : IAggregate<TIn> where TIn : IAppModel
    {
        protected TA Aggregate { get; set; }

        public abstract Task HandleAsync();
        public abstract ICommandHandleAggregate SetAggregate(TIn model);
    }

    // Feature Query

    public interface IQueryHandler<in TIn, TOut> where TIn : IAppModel where TOut : IAppModel
    {
        IQueryHandleAggregate<TOut> SetAggregate(TIn model);
    }

    public interface IQueryHandler<TOut> where TOut : IAppModel
    {
        public Task<TOut> HandleAsync();
    }

    public interface IQueryHandleAggregate<TOut> where TOut : IAppModel
    {
        public Task<TOut> HandleAsync();
    }

    public interface IFeatureQuery<in TIn, TOut> : IQueryHandler<TIn, TOut> where TIn : IAppModel where TOut : IAppModel { }
    public interface IFeatureQuery<TOut> : IQueryHandler<TOut> where TOut : IAppModel { }

    public abstract class AFeatureQueryBase<TA, TIn, TOut> : IFeatureQuery<TIn, TOut>, IQueryHandleAggregate<TOut> where TA : IAggregate<TIn> where TIn : IAppModel where TOut : IAppModel 
    {
        protected TA AggregateModel { get; set; }

        public abstract IQueryHandleAggregate<TOut> SetAggregate(TIn model);

        public abstract Task<TOut> HandleAsync();
    }

    public abstract class AFeaturesQueryBase<TOut> : IFeatureQuery<TOut> where TOut : IAppModel 
    {
        public abstract Task<TOut> HandleAsync();
    }
}