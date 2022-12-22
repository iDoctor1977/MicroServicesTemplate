﻿using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;

namespace CoreServicesTemplate.StorageRoom.Data.DbFrameworks.EntityFramework.FactoryRepositories
{
    public abstract class Creator
    {
        public abstract IRepository FactoryMethod<TIn>(TIn repositoryType) where TIn : IRepository;
    }
}