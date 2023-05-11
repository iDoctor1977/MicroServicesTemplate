using CoreServicesTemplate.Shared.Core.DtoEvents;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IEvents;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class CreateWalletFeature : ICreateWalletFeature
    {
        private readonly IDomainEntityFactory _domainEntityEntityFactory;
        private readonly IEventBus<CreateWalletEventDto> _eventBus;
        private readonly IDefaultMapper<CreateNewWalletAppDto, CreateWalletModel> _walletMapper;
        private readonly ICreateWalletDepot _walletDepot;
        private readonly ILogger<CreateWalletFeature> _logger;

        public CreateWalletFeature(
            IDomainEntityFactory domainEntityEntityFactory,
            IEventBus<CreateWalletEventDto> eventBus,
            IDefaultMapper<CreateNewWalletAppDto, CreateWalletModel> walletMapper,
            ICreateWalletDepot walletDepot,
            ILogger<CreateWalletFeature> logger)
        {
            _domainEntityEntityFactory = domainEntityEntityFactory;
            _eventBus = eventBus;
            _walletMapper = walletMapper;
            _walletDepot = walletDepot;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteAsync(CreateNewWalletAppDto appDto)
        {
            _logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var createWalletModel = _walletMapper.Map(appDto);

            OperationResult operationResult;
            WalletAggregate walletDomainEntity;
            try
            {
                walletDomainEntity = _domainEntityEntityFactory.GenerateAggregate<CreateWalletModel, WalletAggregate>(createWalletModel);
            }
            catch (DomainValidationException<WalletAggregate> e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult(OutcomeState.Failure, default, $" | {e.ClassName}: {e.Message}");
            }

            var walletModel = walletDomainEntity.ToWalletModel();

            try
            {
                operationResult = await _walletDepot.ExecuteAsync(walletModel);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                operationResult = new OperationResult(OutcomeState.Failure, default, $" | Data access failed: {e.Message}");
            }

            _eventBus.Publish(new CreateWalletEventDto { OwnerGuid = appDto.OwnerGuid, IsCreated = true });

            return operationResult;
        }
    }
}