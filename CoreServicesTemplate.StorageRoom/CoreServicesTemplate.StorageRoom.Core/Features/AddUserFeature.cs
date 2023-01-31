using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IResolveMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.SeedWork;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregate;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class AddUserFeature : IAddUserFeature
    {
        private readonly IResolveMapper<UserAppModel, UserAggModel> _userConsolidator;
        private readonly IAddUserDepot _addUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;
        private readonly IAggregateFactory _aggregateFactory;
        private readonly ILogger<AddUserFeature> _logger;

        public AddUserFeature(
            IResolveMapper<UserAppModel, UserAggModel> userConsolidator,
            IAddUserDepot addUserDepot, 
            ISubStepSupplier subStepSupplier,
            IAggregateFactory aggregateFactory,
            ILogger<AddUserFeature> logger)
        {
            _addUserDepot = addUserDepot;
            _subStepSupplier = subStepSupplier;
            _aggregateFactory = aggregateFactory;
            _logger = logger;
            _userConsolidator = userConsolidator;
        }

        public async Task<OperationStatusResult> HandleAsync(UserAppModel @in)
        {
            // decoupling and map modelApp to modelAgg 
            var aggModel = ToData(@in);

            // execute method to aggregate root domain
            var userAggregate = _aggregateFactory.GenerateAggregate<UserAggModel, UserAggregate>(aggModel);
            Console.WriteLine(userAggregate.UserToString());
            Console.WriteLine(userAggregate.AddressToString());

            // decoupling and map modelAgg to modelApp
            var appModel = ToReverseData(aggModel);

            _logger.LogInformation("----- Creating User - User: {@User}", appModel);

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
