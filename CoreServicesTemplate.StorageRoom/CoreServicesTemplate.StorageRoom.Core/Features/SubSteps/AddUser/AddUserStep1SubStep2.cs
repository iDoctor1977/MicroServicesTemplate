using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser
{
    [Leaf(nameof(AddUserStep1))]
    public class AddUserStep1SubStep2 : ISubStep<UserAppModel, UserAppModel>
    {
        private readonly IUserAggregateRoot _userAggregateRoot;
        private readonly IConsolidator<UserAppModel, UserAggModel> _userConsolidator;
        private readonly IAddUserDepot _addUserDepot;

        public AddUserStep1SubStep2(
            IUserAggregateRoot userAggregateRoot, 
            IConsolidator<UserAppModel, UserAggModel> userConsolidator, 
            IAddUserDepot addUserDepot)
        {
            _userAggregateRoot = userAggregateRoot;
            _userConsolidator = userConsolidator;
            _addUserDepot = addUserDepot;
        }

        public async Task<UserAppModel> ExecuteAsync(UserAppModel modelApp)
        {
            // Do anything on User aggregate

            return modelApp;
        }
    }
}