using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetUserEfDepot : EfUnitOfWork, IGetUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IDefaultMapper<UserAppModel, User> _userConsolidator;

        public GetUserEfDepot(
            IDbContextWrap dbContextWrap,
            IDefaultMapper<UserAppModel, User> userConsolidator,
            IUserRepository userRepository) : base(dbContextWrap)
        {
            _userConsolidator = userConsolidator;
            _userRepository = userRepository;
        }

        public async Task<OperationResult<UserAppModel>> ExecuteAsync(UserAppModel model)
        {
            OperationResult<UserAppModel> operationResult;

            User entity = _userConsolidator.Map(model);

            entity = await _userRepository.GetByNameAsync(entity);

            if (entity != null)
            {
                var modelResult = _userConsolidator.Map(entity);

                operationResult = new OperationResult<UserAppModel>(modelResult);
            }
            else
            {
                operationResult = new OperationResult<UserAppModel>("No user found!");
            }

            return operationResult;
        }
    }
}
