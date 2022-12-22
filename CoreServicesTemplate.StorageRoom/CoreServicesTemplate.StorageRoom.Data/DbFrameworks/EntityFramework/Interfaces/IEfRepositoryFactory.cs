using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using System;

namespace CoreServicesTemplate.StorageRoom.Data.DbFrameworks.EntityFramework.Interfaces
{
    public interface IEfRepositoryFactory
    {
        public T CreateRepository<T>() where T : IRepository;
    }
}