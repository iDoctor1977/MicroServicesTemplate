using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Data.Builders;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
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
            var users = builder
                .AddUser("Donald", "Duck", DateTime.Now.AddDays(-13698))
                .AddUser("Foo", "Foo", DateTime.Now.AddDays(-9635))
                .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-7326))
                .Build();

            Entities.AddRange(users);
        }

        public Task<int> CreateEntity(UserApiModel model)
        {
            return Task.FromResult(1);
        }

        public Task<IEnumerable<UserApiModel>> ReadEntities()
        {
            var models = _mapper.Map<IEnumerable<UserApiModel>>(Entities);

            return Task.FromResult(models);
        }

        public Task<UserApiModel> ReadEntityByGuid(Guid guid)
        {
            var entity = Entities.First();
            var model = _mapper.Map<UserApiModel>(entity);

            return Task.FromResult(model);
        }
    }
}