using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Mocks
{
    public class UserEfRepositoryMock : EfRepositoryBaseMock<User>, IUserRepository
    {
        private static readonly List<User> Entities = new List<User>();

        public UserEfRepositoryMock(Lazy<StorageRoomDbContext> dbContext) : base(dbContext)
        {
            var builder = new UserEntityBuilder();

            if (Entities is null || !Entities.Any())
            {
                var users = builder
                    .AddUser("Donald", "Duck", DateTime.Now.AddDays(-13698))
                    .AddUser("Foo", "Foo", DateTime.Now.AddDays(-9635))
                    .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-7326))
                    .AddUser("Jerry", "Scala", DateTime.Now.AddDays(-15326))
                    .Build();

                Entities.AddRange(users);
            }
        }

        public Task AddCustomAsync(User entity)
        {
            Entities.Add(entity);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> GetAllCustomAsync()
        {
            return Task.FromResult<IEnumerable<User>>(Entities);
        }

        public Task UpdateCustomAsync(User entity)
        {
            return Task.CompletedTask;
        }

        public Task<User> GetByNameAsync(User entity)
        {
            return Task.FromResult(Entities.FirstOrDefault(r => r.Name == entity.Name));
        }

        public async Task<User> GetByGuidAsync(User entity)
        {
            try
            {
                if (entity != null)
                {
                    entity = Entities.First();

                    return await Task.FromResult(entity);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return await Task.FromResult<User>(null);
        }

        public async Task<User> GetByIdAsync(User entity)
        {
            try
            {
                if (entity != null)
                {
                    entity = Entities.First();

                    return await Task.FromResult(entity);
                }
            }
            catch (Exception exception)
            {
                throw new DbUpdateException(GetType().FullName + " - " + MethodBase.GetCurrentMethod().Name, exception);
            }

            return await Task.FromResult<User>(null);
        }

        public Task DeleteCustomAsync(User entity)
        {
            return Task.CompletedTask;
        }
    }
}