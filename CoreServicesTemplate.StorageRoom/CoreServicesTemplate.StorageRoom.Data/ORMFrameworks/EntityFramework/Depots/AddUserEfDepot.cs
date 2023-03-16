using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class AddUserEfDepot : UnitOfWorkDepotBase, IAddUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IDefaultMapper<UserAggModel, User> _userMapper;
        private readonly ILogger<AddUserEfDepot> _logger;

        public AddUserEfDepot(
            IAppDbContext dbContext,
            IRepositoryFactory repositoryFactory,
            IDefaultMapper<UserAggModel, User> userMapper,
            ILogger<AddUserEfDepot> logger) : base(repositoryFactory, dbContext)
        {
            _userRepository = RepositoryFactory.GenerateCustomRepository<IUserRepository>(DbContext);
            _userMapper = userMapper;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteAsync(UserAggModel model)
        {
            _logger.LogInformation("----- Creating User: {@Class} {@User} {Dt}", GetType().Name, model.Name, DateTime.UtcNow.ToLongTimeString());

            var entity = _userMapper.Map(model);

            await _userRepository.AddCustomAsync(entity);

            await DbContext.CommitAsync();

            return new OperationResult(OutcomeState.Success);
        }
    }
}
