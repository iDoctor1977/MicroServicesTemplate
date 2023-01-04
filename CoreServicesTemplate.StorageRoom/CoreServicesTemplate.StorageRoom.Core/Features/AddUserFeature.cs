﻿using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class AddUserFeature : IFeatureCommand<UserAppModel>
    {
        private readonly IUserAggregateRoot _userAggregateRoot;
        private readonly IConsolidator<UserAppModel, UserAggModel> _userConsolidator;
        private readonly IAddUserDepot _addUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;

        public AddUserFeature(
            IUserAggregateRoot userAggregateRoot,
            IConsolidator<UserAppModel, UserAggModel> userConsolidator,
            IAddUserDepot addUserDepot, 
            ISubStepSupplier subStepSupplier)
        {
            _userAggregateRoot = userAggregateRoot;
            _addUserDepot = addUserDepot;
            _subStepSupplier = subStepSupplier;
            _userConsolidator = userConsolidator;
        }

        public async Task HandleAsync(UserAppModel @in)
        {
            // decoupling and map modelApp to modelAgg 
            var aggregationModel = _userConsolidator.ToData(@in).Resolve();

            // execute method to aggregate root domain
            aggregationModel = await _userAggregateRoot.CreateUser(aggregationModel);
            await _userAggregateRoot.UserToString();
            await _userAggregateRoot.AddressToString();

            // decoupling and map modelAgg to modelApp
            var appModel = _userConsolidator.ToDataReverse(aggregationModel).Resolve();

            // execute consolidation with repository (if necessary)
            await _addUserDepot.HandleAsync(appModel);

            // execute addUserFeature sub steps
            // this part is added only for features scalability 
            // Ex.: await _subStepSupplier.AddHandleAsync(appModel);
            await _subStepSupplier.AddHandleAsync(appModel);
        }
    }
}
