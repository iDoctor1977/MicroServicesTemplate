using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
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
                var userDomain = _aggregateFactory.GenerateAggregate<UserAggModel, UserAggregate>(aggModel);
                Console.WriteLine(userDomain.UserToString());
                Console.WriteLine(userDomain.AddressToString());
                aggModel = userDomain.ToModel();
            }
            catch (DomainValidationException<UserAggregate> e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult(OutcomeState.Failure, default, e.ClassName + e.Message);
            }

            // execute addUserFeature sub steps
            // this part is added only for features scalability 
            //aggModel = _subStepSupplier.ExecuteAddAsync(aggModel);

            try
            {
                // execute persistence to repository
                return await _addUserDepot.ExecuteAsync(aggModel);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult(OutcomeState.Failure, default, $"Data access failed! {e.Message}");
            }
        }
    }
}
