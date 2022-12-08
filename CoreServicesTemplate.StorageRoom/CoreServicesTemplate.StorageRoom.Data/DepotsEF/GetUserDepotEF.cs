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
    public class GetUserDepotEF : DepotBaseEF, IGetUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsolidators<UserModel, User> _consolidatorsCustomReceiver;
        private readonly IConsolidators<User, UserModel> _consolidatorsCustomPresenter;

        public GetUserDepotEF(
            Lazy<DbContextProject> dbContext,
            IRepositoryFactoryEF repositoryFactory, 
            IConsolidators<User, UserModel> consolidatorsCustomPresenter) : base(dbContext)
        {
            _userRepository = repositoryFactory.CreateRepository<IUserRepository>(dbContext.Value);
            _consolidatorsCustomPresenter = consolidatorsCustomPresenter;
        }
        
        public async Task<UserModel> HandleAsync(UserModel model)
        {
            User entity = _consolidatorsCustomReceiver.ToData(model);

            entity = await _userRepository.GetEntityByName(entity);

            var modelResult = _consolidatorsCustomPresenter.ToData(entity);

            return modelResult;
        }
    }
}
