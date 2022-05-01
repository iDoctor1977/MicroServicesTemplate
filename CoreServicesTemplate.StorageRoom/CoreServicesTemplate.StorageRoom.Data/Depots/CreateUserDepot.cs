using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Data.Depots
{
    public class CreateUserDepot : ICreateUserDepot
    {
        private readonly IUserRepository _userRepository;

        public CreateUserDepot(IServiceProvider service) {
            _userRepository = service.GetRequiredService<IUserRepository>();
        }

        public async Task HandleAsync(UserApiModel model)
        {
            await _userRepository.CreateEntity(model);
        }
    }
}
