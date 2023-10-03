using System;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.Shared.Core.Factories;

public class DomainEntityFactory : IDomainEntityFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEntityFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TOut Generate<TIn, TOut>(TIn model)
    {
        var instance = ActivatorUtilities.CreateInstance<TOut>(_serviceProvider, model);

        return instance;
    }
}