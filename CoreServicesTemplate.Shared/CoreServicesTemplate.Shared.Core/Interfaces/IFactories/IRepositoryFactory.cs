﻿using CoreServicesTemplate.Shared.Core.Interfaces.IData;

namespace CoreServicesTemplate.Shared.Core.Interfaces.IFactories;

public interface IRepositoryFactory
{
    /// <summary>
    /// Generates a standard CRUD repository for the specified entity type.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <returns>A standard CRUD repository.</returns>
    IRepository<T> GenerateDefaultRepositoryFor<T>(IUnitOfWorkContext unitOfWorkContext) where T : IEntityEfBase;

    /// <summary>
    /// Generates a custom repository for the specified interface.
    /// </summary>
    /// <typeparam name="T">The repository's interface.</typeparam>
    /// <returns>Returns a custom repository.</returns>
    T GenerateCustomRepository<T>(IUnitOfWorkContext unitOfWorkContext) where T : IRepository;
}