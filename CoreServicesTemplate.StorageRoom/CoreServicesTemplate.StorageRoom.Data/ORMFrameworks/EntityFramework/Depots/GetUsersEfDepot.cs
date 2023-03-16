using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetUsersEfDepot : UnitOfWorkDepotBase, IGetUsersDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IDefaultMapper<UserAggModel, User> _userMapper;

        public GetUsersEfDepot(
            IAppDbContext dbContext,
            IRepositoryFactory repositoryFactory,
            IDefaultMapper<UserAggModel, User> usersCustomMapper) : base(repositoryFactory, dbContext)
        {
            _userRepository = RepositoryFactory.GenerateCustomRepository<IUserRepository>(DbContext);
            _userMapper = usersCustomMapper;
        }

        public async Task<OperationResult<ICollection<UserAggModel>>> ExecuteAsync()
        {
            OperationResult<ICollection<UserAggModel>> operationResult;

            var entities = await _userRepository.GetAllCustomAsync();

            if (!entities.Equals(null))
            {
                var aggModels = new List<UserAggModel>(_userMapper.Map(entities.ToList()));
                operationResult = new OperationResult<ICollection<UserAggModel>>(aggModels);
            }
            else
            {
                operationResult = new OperationResult<ICollection<UserAggModel>>("There are no users!");
            }

            return operationResult;
        }
    }
}
