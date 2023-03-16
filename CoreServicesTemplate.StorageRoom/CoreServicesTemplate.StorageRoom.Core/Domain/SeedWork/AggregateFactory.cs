using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;

public class AggregateFactory : IAggregateFactory
{
    private readonly IServiceProvider _serviceProvider;

    public AggregateFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TOut GenerateAggregate<TIn, TOut>(TIn model)
    {
        var instance = ActivatorUtilities.CreateInstance<TOut>(_serviceProvider, model);

        return instance;
    }
}