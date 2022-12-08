using System;
using System.Collections.Generic;
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
    public class GetUsersDepotEF : DepotBaseEF, IGetUsersDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsolidators<IEnumerable<User>, UsersModel> _consolidatorsCustomPresenter;

        public GetUsersDepotEF(
            Lazy<DbContextProject> dbContext,
            IRepositoryFactoryEF repositoryFactory, 
            IConsolidators<IEnumerable<User>, UsersModel> consolidatorsCustomPresenter) : base(dbContext)
        {
            _userRepository = repositoryFactory.CreateRepository<IUserRepository>(dbContext.Value);
            _consolidatorsCustomPresenter = consolidatorsCustomPresenter;
        }

        public async Task<UsersModel> HandleAsync()
        {
            var entity = await _userRepository.GetEntities();

            var model = _consolidatorsCustomPresenter.ToData(entity);

            return model;
        }
    }
}
