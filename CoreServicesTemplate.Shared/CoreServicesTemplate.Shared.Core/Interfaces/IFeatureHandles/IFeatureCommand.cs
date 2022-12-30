using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles
{
    #region Feature Command

    public interface ICommandHandler<in TIn> where TIn : IAppModel
    {
        ICommandHandleAggregate SetAggregate(TIn model);
    }

    public interface ICommandHandler<in T1In, in T2In>
        where T1In : IAppModel
        where T2In : IAppModel
    {
        ICommandHandleAggregate SetAggregate(T1In model1, T2In model2);
    }

    public interface ICommandHandleAggregate
    {
        public Task HandleAsync();
    }

    public interface IFeatureCommand<in TIn> : ICommandHandler<TIn> where TIn : IAppModel { }
    public interface IFeatureCommand<in T1In, in T2In> : ICommandHandler<T1In, T2In>
        where T1In : IAppModel
        where T2In : IAppModel
    { }

    #endregion

    #region Feature Query

    public interface IQueryHandler<in TIn, TOut> where TIn : IAppModel where TOut : IAppModel
    {
        IQueryHandleAggregate<TOut> SetAggregate(TIn model);
    }

    public interface IQueryHandler<in T1In, in T2In, TOut> where T1In : IAppModel where T2In : IAppModel where TOut : IAppModel
    {
        IQueryHandleAggregate<TOut> SetAggregate(T1In model1, T2In model2);
    }

    public interface IQueryHandler<TOut> where TOut : IAppModel
    {
        public Task<TOut> HandleAsync();
    }

    public interface IQueryHandleAggregate<TOut> where TOut : IAppModel
    {
        public Task<TOut> HandleAsync();
    }

    public interface IFeatureQuery<TOut> : IQueryHandler<TOut> where TOut : IAppModel { }
    public interface IFeatureQuery<in TIn, TOut> : IQueryHandler<TIn, TOut> where TIn : IAppModel where TOut : IAppModel { }
    public interface IFeatureQuery<in T1In, in T2In, TOut> : IQueryHandler<T1In, T2In, TOut>
        where T1In : IAppModel
        where T2In : IAppModel
        where TOut : IAppModel
    { }

    #endregion
}