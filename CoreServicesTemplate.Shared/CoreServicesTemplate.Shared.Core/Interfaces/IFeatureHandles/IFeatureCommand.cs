using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles
{
    #region Feature Command

    public interface ICommandHandler<in TIn> where TIn : IAppModel
    {
        ICommandHandleAggregate SetModel(TIn model);
    }

    public interface ICommandHandleAggregate
    {
        public Task HandleAsync();
    }

    public interface IFeatureCommand<in TIn> : ICommandHandler<TIn> where TIn : IAppModel { }

    #endregion

    #region Feature Query

    public interface IQueryHandler<in TIn, TOut> where TIn : IAppModel where TOut : IAppModel
    {
        IQueryHandleAggregate<TOut> SetModel(TIn model);
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

    #endregion
}