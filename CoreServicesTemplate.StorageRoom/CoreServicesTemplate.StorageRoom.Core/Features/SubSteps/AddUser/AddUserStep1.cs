using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Attributes;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Features.SubSteps.AddUser
{
    [Root]
    public class AddUserStep1 : RootPipelineBuilder<UserAppModel, UserAppModel>
    {
        private readonly IUserRoot _userAggregateRoot;
        private readonly IConsolidator<UserAppModel, UserAggModel> _userConsolidator;
        private readonly IAddUserDepot _addUserDepot;

        public AddUserStep1(IAddUserDepot addUserDepot, ICustomMapper customMapper, IUserRoot userAggregateRoot, IConsolidator<UserAppModel, UserAggModel> userConsolidator)
        {
            _addUserDepot = addUserDepot;
            _userAggregateRoot = userAggregateRoot;
            _userConsolidator = userConsolidator;
        }

        protected override async Task<UserAppModel> HandleRootStepAsync(UserAppModel appModel)
        {
            // Do anything on User aggregate

            return appModel;
        }
    }
}