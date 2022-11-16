using System.Threading.Tasks;
using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IGenericRepositories;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Data.DepotsEF
{
    public class ReadUsersDepotEF : DepotBase, IReadUsersDepot
    {
        private readonly IUserRepository _userRepository;

        public ReadUsersDepotEF(IMapper mapper, ProjectDbContext dbContext, IRepositoryFactoryEF repositoryFactory) : base(mapper, dbContext)
        {
            _userRepository = repositoryFactory.CreateRepository<IUserRepository>(dbContext);
        }

        public async Task<UsersApiModel> HandleAsync()
        {
            var entity = await _userRepository.ReadEntities();

            await CommitAsync();
            await DisposeAsync();

            var model = Mapper.Map<UsersApiModel>(entity);

            return model;
        }
    }
}
