using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Domain.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser
{
    [Root]
    public class GetUserStep1 : RootPipelineBuilder<UserAppModel, UserAppModel>
    {
        private readonly ICustomMapper<UserAppModel, UserAggModel> _userMapper;
        private readonly IGetUserDepot _getUserDepot;

        public GetUserStep1(
            IGetUserDepot getUserDepot, 
            ICustomMapper<UserAppModel, UserAggModel> userMapper)
        {
            _getUserDepot = getUserDepot;
            _userMapper = userMapper;
        }

        protected override UserAppModel ExecuteRootStepAsync(UserAppModel appModel)
        {
            // Do anything on User aggregate

            return appModel;
        }
    }
}