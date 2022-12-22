using System;
using System.Collections.Generic;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IRepositories;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Data.DbFrameworks.EntityFramework.FactoryRepositories
{
    public class RepositoryFactoryCreator : Creator
    {
        private readonly List<KeyValuePair<string, IRepository>> _repositories;

        public RepositoryFactoryCreator(IUserRepository userRepository)
        {
            _repositories = new List<KeyValuePair<string, IRepository>>
            {
                new (userRepository.GetType().Name, userRepository),
            };
        }

        public override IRepository FactoryMethod<TIn>(TIn repositoryType)
        {
            return _repositories.Find(pair => pair.Key == repositoryType.GetType().Name).Value;
        }
    }
}
