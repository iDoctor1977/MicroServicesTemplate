using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetUsersEfDepot : EfUnitOfWork, IGetUsersDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomMapper<UsersAppModel, IEnumerable<User>> _usersCustomMapper;

        public GetUsersEfDepot(
            StorageRoomDbContext dbContext,
            ICustomMapper<UsersAppModel, IEnumerable<User>> usersCustomMapper,
            IUserRepository userRepository) : base(dbContext)
        {
            _usersCustomMapper = usersCustomMapper;
            _userRepository = userRepository;
        }

        public async Task<UsersAppModel> ExecuteAsync()
        {
            var entity = await _userRepository.GetAllCustomAsync();

            var model = _usersCustomMapper.Map(entity);

            return model;
        }

        public UsersAppModel Execute()
        {
            throw new NotImplementedException();
        }
    }
}
