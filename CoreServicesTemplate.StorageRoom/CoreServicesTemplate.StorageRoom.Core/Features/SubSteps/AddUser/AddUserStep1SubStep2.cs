using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser
{
    [Leaf(nameof(AddUserStep1))]
    public class AddUserStep1SubStep2 : ISubStep<UserAppModel, UserAppModel>
    {
        private readonly IDefaultMapper<UserAppModel, UserAggModel> _userMapper;
        private readonly IAddUserDepot _addUserDepot;

        public AddUserStep1SubStep2(
            IDefaultMapper<UserAppModel, UserAggModel> userMapper, 
            IAddUserDepot addUserDepot)
        {
            _userMapper = userMapper;
            _addUserDepot = addUserDepot;
        }

        public async Task<UserAppModel> ExecuteAsync(UserAppModel modelApp)
        {
            // Do anything on User aggregate

            return modelApp;
        }
    }
}