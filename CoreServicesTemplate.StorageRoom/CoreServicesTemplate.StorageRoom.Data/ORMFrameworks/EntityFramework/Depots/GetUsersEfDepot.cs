using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.AppModels;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
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
            IDbContextWrap dbContextWrap,
            ICustomMapper<UsersAppModel, IEnumerable<User>> usersCustomMapper,
            IUserRepository userRepository) : base(dbContextWrap)
        {
            _usersCustomMapper = usersCustomMapper;
            _userRepository = userRepository;
        }

        public async Task<OperationResult<UsersAppModel>> ExecuteAsync()
        {
            var entity = await _userRepository.GetAllCustomAsync();

            if (entity != null)
            {
                var model = _usersCustomMapper.Map(entity);

                return new OperationResult<UsersAppModel>(model);
            }

            return new OperationResult<UsersAppModel>("There are no users!");
        }
    }
}
