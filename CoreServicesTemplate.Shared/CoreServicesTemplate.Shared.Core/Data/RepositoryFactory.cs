﻿using System;
using CoreServicesTemplate.Shared.Core.Interfaces.IData;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.Shared.Core.Data;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Generates a default repository from the interface.
    /// </summary>
    /// <typeparam name="T">The repository's interface.</typeparam>
    /// <returns>Returns a default repository.</returns>
    public IRepository<T> GenerateDefaultRepositoryFor<T>(IAppDbContext appDbContext) where T : IEntityEfBase
    {
        return ActivatorUtilities.CreateInstance<IRepository<T>>(_serviceProvider, appDbContext);
    }

    /// <summary>
    /// Generates a custom repository for the specified interface.
    /// </summary>
    /// <typeparam name="T">The repository's interface.</typeparam>
    /// <returns>Returns a custom repository.</returns>
    /// <remarks>The concrete implementation of the repository needs to be assigned to the dependency injection container.</remarks>
    public T GenerateCustomRepository<T>(IAppDbContext appDbContext) where T : IRepository
    {
        var concreteType = _serviceProvider.GetRequiredService<T>();

        return (T)ActivatorUtilities.CreateInstance(_serviceProvider, concreteType.GetType(), appDbContext);
    }
}