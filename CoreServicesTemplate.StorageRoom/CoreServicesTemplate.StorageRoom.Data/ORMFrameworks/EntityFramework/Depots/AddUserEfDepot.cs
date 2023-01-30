using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
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
        private readonly IConsolidator<UserAppModel, User> _userModelConsolidator;

        public AddUserEfDepot(
            StorageRoomDbContext dbContext,
            IUserRepository userRepository,
            IConsolidator<UserAppModel, User> userModelConsolidator) : base(dbContext)
        {
            _userModelConsolidator = userModelConsolidator;
            _userRepository = userRepository;
        }

        public async Task<OperationStatusResult> HandleAsync(UserAppModel model)
        {
            var entity = MapUserEntity(model);

            var result = await _userRepository.AddCustomAsync(entity);

            await CommitAsync();

            return result;
        }

        public OperationStatusResult Handle(UserAppModel model)
        {
            var entity = MapUserEntity(model);

            _userRepository.Add(entity);

            Commit();

            return OperationStatusResult.Created;
        }

        private User MapUserEntity(UserAppModel model)
        {
            var entity = _userModelConsolidator.ToData(model).Resolve();

            return entity;
        }
    }
}
