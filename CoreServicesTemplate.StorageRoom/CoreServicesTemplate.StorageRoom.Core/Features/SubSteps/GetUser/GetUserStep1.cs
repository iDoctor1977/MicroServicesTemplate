using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser
{
    [Root]
    public class GetUserStep1 : RootPipelineBuilder<UserAppModel, UserAppModel>
    {
        private readonly IConsolidator<UserAppModel, UserAggModel> _userConsolidator;
        private readonly IGetUserDepot _getUserDepot;

        public GetUserStep1(
            IGetUserDepot getUserDepot, 
            IConsolidator<UserAppModel, UserAggModel> userConsolidator)
        {
            _getUserDepot = getUserDepot;
            _userConsolidator = userConsolidator;
        }

        protected override async Task<UserAppModel> HandleRootStepAsync(UserAppModel appModel)
        {
            // Do anything on User aggregate

            return appModel;
        }
    }
}