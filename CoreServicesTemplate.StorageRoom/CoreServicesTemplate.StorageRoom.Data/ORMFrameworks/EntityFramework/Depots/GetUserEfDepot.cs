﻿using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Bases;

namespace CoreServicesTemplate.StorageRoom.Data.ORMFrameworks.EntityFramework.Depots
{
    public class GetUserEfDepot : EfDepotBase, IGetUserDepot
    {
        private readonly IUserRepository _userRepository;
        private readonly IConsolidatorToData<UserModel, User> _userConsolidator;

        public GetUserEfDepot(
            Lazy<StorageRoomDbContext> dbContext,
            IConsolidatorToData<UserModel, User> userConsolidator,
            IUserRepository userRepository) : base(dbContext)
        {
            _userConsolidator = userConsolidator;
            _userRepository = userRepository;
        }

        public async Task<UserModel> HandleAsync(UserModel model)
        {
            User entity = _userConsolidator.ToData(model).Resolve();

            entity = await _userRepository.GetByNameAsync(entity);

            var modelResult = _userConsolidator.ToDataReverse(entity).Resolve();

            return modelResult;
        }
    }
}
