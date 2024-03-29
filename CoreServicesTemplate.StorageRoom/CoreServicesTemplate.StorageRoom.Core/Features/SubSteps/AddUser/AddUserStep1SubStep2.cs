﻿using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser
{
    [Leaf(nameof(AddUserStep1))]
    public class AddUserStep1SubStep2 : ISubStep<UserAppModel, UserAppModel>
    {
        private readonly ICustomMapper<UserAppModel, UserAggModel> _userMapper;
        private readonly IAddUserDepot _addUserDepot;

        public AddUserStep1SubStep2(
            ICustomMapper<UserAppModel, UserAggModel> userMapper, 
            IAddUserDepot addUserDepot)
        {
            _userMapper = userMapper;
            _addUserDepot = addUserDepot;
        }

        public UserAppModel ExecuteAsync(UserAppModel modelApp)
        {
            // Do anything on User aggregate

            return modelApp;
        }
    }
}