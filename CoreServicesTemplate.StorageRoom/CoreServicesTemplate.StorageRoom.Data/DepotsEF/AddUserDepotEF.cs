using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Data.DepotsEF
{
    public class AddUserDepotEF : DepotBaseEF, IAddUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsolidatorToData<UserModel, User> _userModelReceiver;

        public AddUserDepotEF(
            Lazy<DbContextProject> dbContext,
            IRepositoryFactoryEF repositoryFactory,
            IConsolidatorToData<UserModel, User> userModelReceiver) : base(dbContext)
        {
            _userModelReceiver = userModelReceiver;
            _userRepository = repositoryFactory.CreateRepository<IUserRepository>(dbContext.Value);
        }

        public async Task HandleAsync(UserModel model)
        {
            var entity = _userModelReceiver.ToData(model).Resolve();

            await _userRepository.AddEntity(entity);

            await CommitAsync();
        }
    }
}
