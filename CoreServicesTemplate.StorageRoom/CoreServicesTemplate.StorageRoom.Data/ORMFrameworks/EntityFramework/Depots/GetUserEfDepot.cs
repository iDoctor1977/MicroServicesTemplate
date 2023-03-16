using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
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
        private readonly IDefaultMapper<UserAggModel, User> _userMapper;

        public GetUserEfDepot(
            IAppDbContext dbContext,
            IRepositoryFactory repositoryFactory,
            IDefaultMapper<UserAggModel, User> userMapper) : base(repositoryFactory, dbContext)
        {
            _userRepository = repositoryFactory.GenerateCustomRepository<IUserRepository>(DbContext);
            _userMapper = userMapper;
        }

        public async Task<OperationResult<UserAggModel>> ExecuteAsync(UserAggModel model)
        {
            OperationResult<UserAggModel> operationResult;

            User entity = _userMapper.Map(model);

            entity = await _userRepository.GetByNameAsync(entity);

            if (!entity.Equals(null))
            {
                var modelResult = _userMapper.Map(entity);

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
