﻿using System;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Data.RepositoriesEF
{
    public class RepositoryFactoryEF : IRepositoryFactoryEF
    {
        private readonly IServiceProvider _serviceProvider;

        public RepositoryFactoryEF(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T CreateRepository<T>(DbContextProject dbContext) where T : IRepository
        {
            return ActivatorUtilities.CreateInstance<T>(_serviceProvider, dbContext);
        }
    }
}
