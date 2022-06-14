using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Data.Depots
{
    public class CreateUserDepot : ADepotBase, ICreateUserDepot
    {
        private readonly IUserRepository _userRepository;

        public CreateUserDepot(IServiceProvider service) : base(service)
        {
            _userRepository = service.GetRequiredService<IUserRepository>();
        }

        public async Task HandleAsync(UserApiModel model)
        {
            var entity = Mapper.Map<User>(model);

            await _userRepository.CreateEntity(entity);
        }
    }
}
