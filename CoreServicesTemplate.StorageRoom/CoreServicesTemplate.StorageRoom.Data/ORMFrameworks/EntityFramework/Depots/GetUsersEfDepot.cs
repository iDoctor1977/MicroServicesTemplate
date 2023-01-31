using CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;
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
        private readonly IResolveMapper<UsersAppModel, IEnumerable<User>> _usersConsolidator;

        public GetUsersEfDepot(
            StorageRoomDbContext dbContext,
            IResolveMapper<UsersAppModel, IEnumerable<User>> usersConsolidator,
            IUserRepository userRepository) : base(dbContext)
        {
            _usersConsolidator = usersConsolidator;
            _userRepository = userRepository;
        }

        public async Task<UsersAppModel> HandleAsync()
        {
            var entity = await _userRepository.GetAllCustomAsync();

            var model = _usersConsolidator.ToDataReverse(entity).Resolve();

            return model;
        }

        public UsersAppModel Handle()
        {
            throw new NotImplementedException();
        }
    }
}
