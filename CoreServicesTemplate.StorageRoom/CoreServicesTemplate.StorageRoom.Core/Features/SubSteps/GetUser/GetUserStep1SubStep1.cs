using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser
{
    [Leaf(nameof(GetUserStep1))]
    public class GetUserStep1SubStep1 : ISubStep<UserAppModel, UserAppModel>
    {
        private readonly IMapping<UserAppModel, UserAggModel> _userMapper;
        private readonly IGetUserDepot _getUserDepot;

        public GetUserStep1SubStep1(
            IMapping<UserAppModel, UserAggModel> userMapper, 
            IGetUserDepot getUserDepot)
        {
            _userMapper = userMapper;
            _getUserDepot = getUserDepot;
        }

        public async Task<UserAppModel> ExecuteAsync(UserAppModel modelApp)
        {
            // Do anything on User aggregate

            return modelApp;
        }
    }
}