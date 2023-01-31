﻿using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetUsersEfDepot : EfUnitOfWork, IGetUsersDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IDefaultMapper<UsersAppModel, IEnumerable<User>> _usersMapper;

        public GetUsersEfDepot(
            StorageRoomDbContext dbContext,
            IDefaultMapper<UsersAppModel, IEnumerable<User>> usersMapper,
            IUserRepository userRepository) : base(dbContext)
        {
            _usersMapper = usersMapper;
            _userRepository = userRepository;
        }

        public async Task<UsersAppModel> HandleAsync()
        {
            var entity = await _userRepository.GetAllCustomAsync();

            var model = _usersMapper.Map(entity);

            return model;
        }

        public UsersAppModel Handle()
        {
            throw new NotImplementedException();
        }
    }
}
