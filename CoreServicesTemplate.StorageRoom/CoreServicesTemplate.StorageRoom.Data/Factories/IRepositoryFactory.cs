﻿using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.SeedWorks;

namespace CoreServicesTemplate.StorageRoom.Data.Factories;

public interface IRepositoryFactory
{
    /// <summary>
    /// Generates a standard CRUD repository for the specified entity type.
    /// </summary>
    /// <typeparam name="T">The entity type.</typeparam>
    /// <returns>A standard CRUD repository.</returns>
    IRepository<T> GenerateDefaultRepositoryFor<T>(IAppDbContext appDbContext) where T : EntityBase;

    /// <summary>
    /// Generates a custom repository for the specified interface.
    /// </summary>
    /// <typeparam name="T">The repository's interface.</typeparam>
    /// <returns>Returns a custom repository.</returns>
    T GenerateCustomRepository<T>(IAppDbContext appDbContext) where T : IRepository;
}