using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates.UserAggregates;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using CoreServicesTemplate.StorageRoom.Core.Domain.Models;
using CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class AddUserFeature : IAddUserFeature
    {
        private readonly ICustomMapper<UserAppModel, UserAggModel> _userCustomMapper;
        private readonly IAddUserDepot _addUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;
        private readonly IAggregateFactory _aggregateFactory;
        private readonly ILogger<AddUserFeature> _logger;

        public AddUserFeature(
            ICustomMapper<UserAppModel, UserAggModel> userCustomMapper,
            IAddUserDepot addUserDepot, 
            ISubStepSupplier subStepSupplier,
            IAggregateFactory aggregateFactory,
            ILogger<AddUserFeature> logger)
        {
            _addUserDepot = addUserDepot;
            _subStepSupplier = subStepSupplier;
            _aggregateFactory = aggregateFactory;
            _logger = logger;
            _userCustomMapper = userCustomMapper;
        }

        public async Task<OperationResult> ExecuteAsync(UserAppModel @in)
        {
            _logger.LogInformation("----- Creating User: {@Class} {@User} {Dt}", GetType().Name, @in.Name, DateTime.UtcNow.ToLongTimeString());

            // decoupling and map modelApp to modelAgg 
            var aggModel = _userCustomMapper.Map(@in);

            try
            {
                // generate aggregate instance and execute method on aggregate root domain
                var userAggregate = _aggregateFactory.GenerateAggregate<UserAggModel, UserAggregate>(aggModel);
                Console.WriteLine(userAggregate.UserToString());
                Console.WriteLine(userAggregate.AddressToString());
                aggModel = userAggregate.ToModel();
            }
            catch (UserDomainException e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult(OutcomeState.Error, $"Domain access failed! {e.Message}");
            }

            // decoupling and map modelAgg to modelApp
            var appModel = _userCustomMapper.Map(aggModel);

            // execute addUserFeature sub steps
            // this part is added only for features scalability 
            appModel = _subStepSupplier.ExecuteAddAsync(appModel);

            try
            {
                // execute persistence to repository
                return await _addUserDepot.ExecuteAsync(appModel);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult(OutcomeState.Error, $"Data access failed! {e.Message}");
            }
        }
    }
}
