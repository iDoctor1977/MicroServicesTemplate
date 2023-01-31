﻿using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser
{
    [Root]
    public class GetUserStep1 : RootPipelineBuilder<UserAppModel, UserAppModel>
    {
        private readonly IDefaultMapper<UserAppModel, UserAggModel> _userMapper;
        private readonly IGetUserDepot _getUserDepot;

        public GetUserStep1(
            IGetUserDepot getUserDepot, 
            IDefaultMapper<UserAppModel, UserAggModel> userMapper)
        {
            _getUserDepot = getUserDepot;
            _userMapper = userMapper;
        }

        protected override async Task<UserAppModel> HandleRootStepAsync(UserAppModel appModel)
        {
            // Do anything on User aggregate

            return appModel;
        }
    }
}