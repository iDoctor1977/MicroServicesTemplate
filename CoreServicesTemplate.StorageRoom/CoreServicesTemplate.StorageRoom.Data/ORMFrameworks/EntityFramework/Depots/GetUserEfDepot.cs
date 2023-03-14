using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetUserEfDepot : UnitOfWorkDepotBase, IGetUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IDefaultMapper<UserAggModel, User> _userConsolidator;

        public GetUserEfDepot(
            IDbContextWrap dbContextWrap,
            IDefaultMapper<UserAggModel, User> userConsolidator,
            IUserRepository userRepository) : base(dbContextWrap)
        {
            _userConsolidator = userConsolidator;
            _userRepository = userRepository;
        }

        public async Task<OperationResult<UserAggModel>> ExecuteAsync(UserAggModel model)
        {
            OperationResult<UserAggModel> operationResult;

            User entity = _userConsolidator.Map(model);

            entity = await _userRepository.GetByNameAsync(entity);

            if (!entity.Equals(null))
            {
                var modelResult = _userConsolidator.Map(entity);

                operationResult = new OperationResult<UserAggModel>(modelResult);
            }
            else
            {
                operationResult = new OperationResult<UserAggModel>("No user found!");
            }

            return operationResult;
        }
    }
}
