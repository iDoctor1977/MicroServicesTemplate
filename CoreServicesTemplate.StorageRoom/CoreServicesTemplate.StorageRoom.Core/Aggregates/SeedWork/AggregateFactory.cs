using CoreServicesTemplate.Shared.Core.Interfaces.IAggregates;
using CoreServicesTemplate.Shared.Core.Interfaces.IModels;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.SeedWork;

public class AggregateFactory : IAggregateFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AggregateFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TOut GenerateAggregate<TIn, TOut>(TIn model) where TIn : IAggModel where TOut : IAggregate
    {
        var instance = ActivatorUtilities.CreateInstance<TOut>(_serviceProvider, model);

        return instance;
    }
}