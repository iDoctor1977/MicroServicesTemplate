using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetUsersEfDepot : EfDepotBase, IGetUsersDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsolidator<UsersModel, IEnumerable<User>> _usersConsolidator;

        public GetUsersEfDepot(
            Lazy<StorageRoomDbContext> dbContext,
            IConsolidator<UsersModel, IEnumerable<User>> usersConsolidator,
            IUserRepository userRepository) : base(dbContext)
        {
            _usersConsolidator = usersConsolidator;
            _userRepository = userRepository;
        }

        public async Task<UsersModel> HandleAsync()
        {
            var entity = await _userRepository.GetAllCustomAsync();

            var model = _usersConsolidator.ToDataReverse(entity).Resolve();

            return model;
        }
    }
}
