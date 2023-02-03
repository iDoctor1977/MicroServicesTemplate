using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser
{
    [Root]
    public class AddUserStep1 : RootPipelineBuilder<UserAppModel, UserAppModel>
    {
        private readonly ICustomMapper<UserAppModel, UserAggModel> _userMapper;
        private readonly IAddUserDepot _addUserDepot;

        public AddUserStep1(
            IAddUserDepot addUserDepot, 
            ICustomMapper<UserAppModel, UserAggModel> userMapper)
        {
            _addUserDepot = addUserDepot;
            _userMapper = userMapper;
        }

        protected override async Task<UserAppModel> HandleRootStepAsync(UserAppModel appModel)
        {
            // Do anything on User aggregate

            return appModel;
        }
    }
}