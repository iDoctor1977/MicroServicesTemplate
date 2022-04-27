using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Data.Depots
{
    public class ReadUsersDepot : IReadUsersDepot
    {
        private readonly IUserRepository _userRepository;

        public ReadUsersDepot(IServiceProvider service) {
            _userRepository = service.GetRequiredService<IUserRepository>();
        }

        public async Task<IEnumerable<UserApiModel>> ExecuteAsync(object model)
        {
            var models = await _userRepository.ReadEntities();

            return models;
        }
    }
}
