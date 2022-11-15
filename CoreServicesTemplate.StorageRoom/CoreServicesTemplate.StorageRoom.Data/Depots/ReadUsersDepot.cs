using System.Threading.Tasks;
using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IRepositories;

namespace CoreServicesTemplate.StorageRoom.Data.Depots
{
    public class ReadUsersDepot : ADepotBase, IReadUsersDepot
    {
        private readonly IUserRepository _userRepository;

        public ReadUsersDepot(IMapper mapper, IUserRepository userRepository) : base(mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<UsersApiModel> HandleAsync()
        {
            var entity = await _userRepository.ReadEntities();

            var model = Mapper.Map<UsersApiModel>(entity);

            return model;
        }
    }
}
