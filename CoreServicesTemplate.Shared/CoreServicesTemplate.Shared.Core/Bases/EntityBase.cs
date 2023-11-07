using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Shared.Core.Bases;

public abstract class EntityBase<TModel, TEntity> : IAggregate where TModel : IModelBase where TEntity : IAggregate
{
    protected readonly IDomainEntityFactory FactoryBase;
    protected readonly IDefaultMapper<TModel, TEntity> MapperBase;
    protected readonly ILogger<TEntity> LoggerBase;

    protected EntityBase(
        IDomainEntityFactory factoryBase, 
        IDefaultMapper<TModel, TEntity> mapperBase, 
        ILogger<TEntity> loggerBase)
    {
        FactoryBase = factoryBase;
        MapperBase = mapperBase;
        LoggerBase = loggerBase;
    }

    protected abstract void SharedConstruction(IModelBase modelBase);

    public abstract TModel ToModel();
}