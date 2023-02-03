using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class AddUserEfDepot : EfUnitOfWork, IAddUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IDefaultMapper<UserAppModel, User> _userMapper;

        public AddUserEfDepot(
            StorageRoomDbContext dbContext,
            IUserRepository userRepository,
            IDefaultMapper<UserAppModel, User> userMapper) : base(dbContext)
        {
            _userMapper = userMapper;
            _userRepository = userRepository;
        }

        public async Task<OperationStatusResult> ExecuteAsync(UserAppModel model)
        {
            var entity = _userMapper.Map(model);

            var result = await _userRepository.AddCustomAsync(entity);

            await CommitAsync();

            return result;
        }

        public OperationStatusResult Execute(UserAppModel model)
        {
            var entity = _userMapper.Map(model);

            _userRepository.Add(entity);

            Commit();

            return OperationStatusResult.Created;
        }
    }
}
