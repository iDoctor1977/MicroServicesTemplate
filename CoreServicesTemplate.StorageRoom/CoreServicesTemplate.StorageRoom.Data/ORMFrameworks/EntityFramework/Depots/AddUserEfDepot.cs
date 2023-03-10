using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class AddUserEfDepot : EfUnitOfWork, IAddUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IDefaultMapper<UserAppModel, User> _userMapper;
        private readonly ILogger<AddUserEfDepot> _logger;

        public AddUserEfDepot(
            IDbContextWrap dbContextWrap,
            IDefaultMapper<UserAppModel, User> userMapper,
            IUserRepository userRepository, 
            ILogger<AddUserEfDepot> logger) : base(dbContextWrap)
        {
            _userMapper = userMapper;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteAsync(UserAppModel model)
        {
            _logger.LogInformation("----- Creating User: {@Class} {@User} {Dt}", GetType().Name, model.Name, DateTime.UtcNow.ToLongTimeString());

            var entity = _userMapper.Map(model);

            await _userRepository.AddCustomAsync(entity);

            await CommitAsync();

            return new OperationResult(OutcomeState.Success);
        }
    }
}
