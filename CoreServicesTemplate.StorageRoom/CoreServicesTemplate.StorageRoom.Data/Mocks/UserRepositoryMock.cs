using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Data.Mocks
{
    public class UserRepositoryMock : IUserRepository
    {
        private IConfiguration Configuration { get; }
        private readonly IMapper _mapper;

        private static readonly List<User> Entities = new List<User>();

        public UserRepositoryMock(IServiceProvider service, IConfiguration configuration)
        {
            Configuration = configuration;
            _mapper = service.GetRequiredService<IMapper>();

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

        public Task<int> CreateEntity(User entity)
        {
            return Task.FromResult(1);
        }

        public Task<IEnumerable<User>> ReadEntities()
        {
            return Task.FromResult<IEnumerable<User>>(Entities);
        }

        public async Task<User> ReadEntityByGuid(User entity)
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
    }
}