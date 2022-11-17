using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoreServicesTemplate.StorageRoom.Data.Mocks
{
    public class UserRepositoryMock : RepositoryBaseEF<User>, IUserRepository
    {
        private IConfiguration Configuration { get; }
        private readonly IMapper _mapper;

        private static readonly List<User> Entities = new List<User>();

        public UserRepositoryMock(DbContextProject dbContext, IMapper mapper, IConfiguration configuration) : base(dbContext)
        {
            Configuration = configuration;
            _mapper = mapper;

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

        public Task CreateEntity(User entity)
        {
            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> ReadEntities()
        {
            return Task.FromResult<IEnumerable<User>>(Entities);
        }

        public Task UpdateEntity(User entity)
        {
            return Task.CompletedTask;
        }

        public Task<User> ReadEntityByName(User entity)
        {
            return Task.FromResult(Entities.FirstOrDefault(r => r.Name == entity.Name));
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

        public Task DeleteEntity(User entity)
        {
            return Task.CompletedTask;
        }

        public new User Get(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public new IEnumerable<User> GetAll()
        {
            return Entities;
        }

        public new IEnumerable<User> GetAll(Expression<Func<User, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public new void Add(User entity)
        {
            Entities.Add(entity);
        }

        public new Task AddAsync(User entity)
        {
            return Task.CompletedTask;
        }

        public new void AddRange(IEnumerable<User> entities)
        {
            Entities.AddRange(entities);
        }

        public new void Remove(User entity)
        {
            Entities.Remove(entity);
        }

        public new void RemoveRange(IEnumerable<User> entities)
        {
            Entities.RemoveAll(entities.Contains);
        }

        public new void Update(User entity)
        {
            var result = Entities.FirstOrDefault(u => u.Guid == entity.Guid);

            if (result != null)
            {
                result.Name = entity.Name;
                result.Surname = entity.Surname;
                result.Birth = entity.Birth;
            }
        }

        public new void UpdateRange(IEnumerable<User> entities)
        {
            throw new NotImplementedException();
        }

        public new Task<User> GetAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public new Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public new Task AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public new Task AddRangeAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}