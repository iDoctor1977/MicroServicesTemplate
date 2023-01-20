using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandlers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class AddUserFeature : IQueryHandlerFeature<UserAppModel, OperationStatusResult>
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

        public async Task<OperationStatusResult> HandleAsync(UserAppModel @in)
        {
            // decoupling and map modelApp to modelAgg 
            var aggregationModel = ToData(@in);

            // execute method to aggregate root domain
            aggregationModel = await _userAggregateRoot.CreateUser(aggregationModel);
            await _userAggregateRoot.UserToString();
            await _userAggregateRoot.AddressToString();

            // decoupling and map modelAgg to modelApp
            var appModel = ToReverseData(aggregationModel);

            // execute consolidation with repository (if necessary)
            var result = await _addUserDepot.HandleAsync(appModel);

            // execute addUserFeature sub steps
            // this part is added only for features scalability 
            // Ex.: await _subStepSupplier.AddHandleAsync(appModel);
            await _subStepSupplier.AddHandleAsync(appModel);

            return result;
        }

        public OperationStatusResult Handle(UserAppModel @in)
        {
            throw new System.NotImplementedException();
        }

        private UserAggModel ToData(UserAppModel @in)
        {
            var aggModel = _userConsolidator.ToData(@in).Resolve();

            return aggModel;
        }

        private UserAppModel ToReverseData(UserAggModel @in)
        {
            var appModel = _userConsolidator.ToDataReverse(@in).Resolve();

            return appModel;
        }
    }
}
