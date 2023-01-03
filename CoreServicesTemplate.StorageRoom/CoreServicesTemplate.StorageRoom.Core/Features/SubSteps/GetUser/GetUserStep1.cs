using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.GetUser
{
    [Root]
    public class GetUserStep1 : RootPipelineBuilder<UserAppModel, UserAppModel>
    {
        private readonly IUserRoot _userAggregateRoot;
        private readonly IConsolidator<UserAppModel, UserAggModel> _userConsolidator;
        private readonly IGetUserDepot _getUserDepot;

        public GetUserStep1(IGetUserDepot getUserDepot, IConsolidator<UserAppModel, UserAggModel> userConsolidator, IUserRoot userAggregateRoot)
        {
            _getUserDepot = getUserDepot;
            _userConsolidator = userConsolidator;
            _userAggregateRoot = userAggregateRoot;
        }

        protected override async Task<UserAppModel> HandleRootStepAsync(UserAppModel appModel)
        {
            // Do anything on User aggregate

            return appModel;
        }
    }
}