using System.Threading.Tasks;
using AutoMapper;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Data.Bases;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces.IGenericRepositories;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF;
using CoreServicesTemplate.StorageRoom.Data.RepositoriesEF.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Data.DepotsEF
{
    public class CreateUserDepotEF : DepotBase, ICreateUserDepot
    {
        private readonly IUserRepository _userRepository;

        public CreateUserDepotEF(IMapper mapper, ProjectDbContext dbContext, IRepositoryFactoryEF repositoryFactory) : base(mapper, dbContext)
        {
            _userRepository = repositoryFactory.CreateRepository<IUserRepository>(dbContext);
        }

        public async Task HandleAsync(UserApiModel model)
        {
            var entity = Mapper.Map<User>(model);

            await _userRepository.CreateEntity(entity);

            await CommitAsync();
            await DisposeAsync();
        }
    }
}
