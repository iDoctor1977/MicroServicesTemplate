using System;
using System.Threading.Tasks;
using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Data.DepotsEF
{
    public class ReadUsersDepotEF : DepotBaseEF, IReadUsersDepot
    {
        private readonly IUserRepository _userRepository;

        public ReadUsersDepotEF(IMapper mapper, Lazy<DbContextProject> dbContext, IRepositoryFactoryEF repositoryFactory) : base(mapper, dbContext)
        {
            _userRepository = repositoryFactory.CreateRepository<IUserRepository>(dbContext.Value);
        }

        public async Task<UsersApiModel> HandleAsync()
        {
            var entity = await _userRepository.ReadEntities();

            await CommitAsync();

            var model = Mapper.Map<UsersApiModel>(entity);

            return model;
        }
    }
}
