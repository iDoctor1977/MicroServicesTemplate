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
    [Leaf(nameof(GetUserStep1))]
    public class GetUserStep1SubStep1 : ISubStep<UserAppModel, UserAppModel>
    {
        private readonly IUserRoot _userAggregateRoot;
        private readonly IConsolidator<UserAppModel, UserAggModel> _userConsolidator;
        private readonly IGetUserDepot _getUserDepot;

        public GetUserStep1SubStep1(IUserRoot userAggregateRoot, IConsolidator<UserAppModel, UserAggModel> userConsolidator, IGetUserDepot getUserDepot)
        {
            _userAggregateRoot = userAggregateRoot;
            _userConsolidator = userConsolidator;
            _getUserDepot = getUserDepot;
        }

        public async Task<UserAppModel> ExecuteAsync(UserAppModel modelApp)
        {
            // Do anything on User aggregate

            return modelApp;
        }
    }
}