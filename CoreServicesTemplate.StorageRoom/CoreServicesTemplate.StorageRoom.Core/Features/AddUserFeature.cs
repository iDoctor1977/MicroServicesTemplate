using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.SeedWork;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.UserAggregates;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class AddUserFeature : IAddUserFeature
    {
        private readonly IMapperService<UserAppModel, UserAggModel> _userMapper;
        private readonly IAddUserDepot _addUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;
        private readonly IAggregateFactory _aggregateFactory;
        private readonly ILogger<AddUserFeature> _logger;

        public AddUserFeature(
            IMapperService<UserAppModel, UserAggModel> userMapper,
            IAddUserDepot addUserDepot, 
            ISubStepSupplier subStepSupplier,
            IAggregateFactory aggregateFactory,
            ILogger<AddUserFeature> logger)
        {
            _addUserDepot = addUserDepot;
            _subStepSupplier = subStepSupplier;
            _aggregateFactory = aggregateFactory;
            _logger = logger;
            _userMapper = userMapper;
        }

        public async Task<OperationStatusResult> ExecuteAsync(UserAppModel @in)
        {
            // decoupling and map modelApp to modelAgg 
            var aggModel = _userMapper.Map(@in);

            // generate aggregate instance and execute method on aggregate root domain
            var userAggregate = _aggregateFactory.GenerateAggregate<UserAggModel, UserAggregate>(aggModel);
            Console.WriteLine(userAggregate.UserToString());
            Console.WriteLine(userAggregate.AddressToString());
            aggModel = userAggregate.CreateUser(aggModel);

            // decoupling and map modelAgg to modelApp
            var appModel = _userMapper.Map(aggModel);

            _logger.LogInformation("----- Creating User: {@User} {Dt}", appModel.Name, DateTime.UtcNow.ToLongTimeString());

            // execute consolidation with repository (if necessary)
            var result = await _addUserDepot.ExecuteAsync(appModel);

            // execute addUserFeature sub steps
            // this part is added only for features scalability 
            // Ex.: await _subStepSupplier.AddHandleAsync(appModel);
            //await _subStepSupplier.AddHandleAsync(appModel);

            return result;
        }

        public OperationStatusResult Execute(UserAppModel @in)
        {
            throw new NotImplementedException();
        }
    }
}
