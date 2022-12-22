﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.DbFrameworks.EntityFramework.Bases;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Data.DbFrameworks.EntityFramework.Depots
{
    public class GetUsersEfDepot : EfDepotBase, IGetUsersDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsolidatorToData<UsersModel, IEnumerable<User>> _usersConsolidator;

        public GetUsersEfDepot(
            Lazy<StorageRoomDbContext> dbContext,
            IConsolidatorToData<UsersModel, IEnumerable<User>> usersConsolidator, 
            IUserRepository userRepository) : base(dbContext)
        {
            _usersConsolidator = usersConsolidator;
            _userRepository = userRepository;
        }

        public async Task<UsersModel> HandleAsync()
        {
            var entity = await _userRepository.GetEntities();

            var model = _usersConsolidator.ToDataReverse(entity).Resolve();

            return model;
        }
    }
}