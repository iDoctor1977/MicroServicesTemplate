using CoreServicesTemplate.Shared.Core.BusModels.Wallet;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IEvents;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.Wallet;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class CreateWalletFeature : ICreateWalletFeature
    {
        private readonly IDomainEntityFactory _domainEntityFactory;
        private readonly IEventBus<WalletCreatedBusDto> _eventBus;
        private readonly IDefaultMapper<CreateWalletAppDto, CreateWalletModel> _walletMapper;
        private readonly ICreateWalletDepot _walletDepot;
        private readonly ILogger<CreateWalletFeature> _logger;

        public CreateWalletFeature(
            IDomainEntityFactory domainEntityEntityFactory,
            IEventBus<WalletCreatedBusDto> eventBus,
            IDefaultMapper<CreateWalletAppDto, CreateWalletModel> walletMapper,
            ICreateWalletDepot walletDepot,
            ILogger<CreateWalletFeature> logger)
        {
            _domainEntityFactory = domainEntityEntityFactory;
            _eventBus = eventBus;
            _walletMapper = walletMapper;
            _walletDepot = walletDepot;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteAsync(CreateWalletAppDto app)
        {
            _logger.LogInformation("----- Execute feature: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var baseWalletModel = _walletMapper.Map(app);

            OperationResult operationResult;
            WalletAggregate walletDomainEntity;
            try
            {
                walletDomainEntity = _domainEntityFactory.Generate<CreateWalletModel, WalletAggregate>(baseWalletModel);
            }
            catch (DomainValidationException<WalletAggregate> e)
            {
                _logger.LogCritical(e.Message);

                return new OperationResult(OutcomeState.Failure, default, $"{e.ClassName}: {e.Message}");
            }

            var walletModel = walletDomainEntity.ToWalletModel();

            try
            {
                operationResult = await _walletDepot.ExecuteAsync(walletModel);

                // Send payload to RabbitMq event bus 
                _eventBus.Publish(new WalletCreatedBusDto { OwnerGuid = app.OwnerGuid, IsCreated = true });
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);

                operationResult = new OperationResult(OutcomeState.Failure, default, $" | Data access failed: {e.Message}");
            }

            return operationResult;
        }
    }
}