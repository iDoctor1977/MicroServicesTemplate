using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Data.Depots
{
    public class ReadUsersDepot : ADepotBase, IReadUsersDepot
    {
        private readonly IUserRepository _userRepository;

        public ReadUsersDepot(IServiceProvider service) : base(service)
        {
            _userRepository = service.GetRequiredService<IUserRepository>();
        }

        public async Task<UsersApiModel> HandleAsync()
        {
            var entity = await _userRepository.ReadEntities();

            var model = Mapper.Map<UsersApiModel>(entity);

            return model;
        }
    }
}
