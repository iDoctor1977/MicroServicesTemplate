﻿using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.DbFrameworks.EntityFramework.Bases;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Data.DbFrameworks.EntityFramework.Depots
{
    public class AddUserEfDepot : EfDepotBase, IAddUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsolidatorToData<UserModel, User> _userModelConsolidator;

        public AddUserEfDepot(
            Lazy<StorageRoomDbContext> dbContext,
            IUserRepository userRepository,
            IConsolidatorToData<UserModel, User> userModelConsolidator) : base(dbContext)
        {
            _userModelConsolidator = userModelConsolidator;
            _userRepository = userRepository;
        }

        public async Task HandleAsync(UserModel model)
        {
            var entity = _userModelConsolidator.ToData(model).Resolve();

            await _userRepository.AddEntity(entity);

            await CommitAsync();
        }
    }
}