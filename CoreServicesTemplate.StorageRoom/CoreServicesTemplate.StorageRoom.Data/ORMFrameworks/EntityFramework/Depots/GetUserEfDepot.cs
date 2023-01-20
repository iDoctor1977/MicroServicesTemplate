using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetUserEfDepot : EfUnitOfWork, IGetUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsolidator<UserAppModel, User> _userConsolidator;

        public GetUserEfDepot(
            Lazy<StorageRoomDbContext> dbContext,
            IConsolidator<UserAppModel, User> userConsolidator,
            IUserRepository userRepository) : base(dbContext)
        {
            _userConsolidator = userConsolidator;
            _userRepository = userRepository;
        }

        public async Task<UserAppModel> HandleAsync(UserAppModel model)
        {
            User entity = _userConsolidator.ToData(model).Resolve();

            entity = await _userRepository.GetByNameAsync(entity);

            var modelResult = _userConsolidator.ToDataReverse(entity).Resolve();

            return modelResult;
        }

        public UserAppModel Handle(UserAppModel model)
        {
            throw new NotImplementedException();
        }
    }
}
