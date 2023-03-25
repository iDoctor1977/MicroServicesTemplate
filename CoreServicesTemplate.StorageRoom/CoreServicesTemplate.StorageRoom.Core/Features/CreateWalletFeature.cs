using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class CreateWalletFeature : ICreateWalletFeature
    {
        private readonly IDomainFactory _domainEntityFactory;
        private readonly IDefaultMapper<CreateWalletAppDto, CreateWalletModel> _walletMapper;
        private readonly ICreateWalletDepot _walletDepot;
        private readonly ILogger<CreateWalletFeature> _logger;

        public CreateWalletFeature(
            IDomainFactory domainEntityFactory,
            IDefaultMapper<CreateWalletAppDto, CreateWalletModel> walletMapper,
            ICreateWalletDepot walletDepot, ILogger<CreateWalletFeature> logger)
        {
            _domainEntityFactory = domainEntityFactory;
            _walletMapper = walletMapper;
            _walletDepot = walletDepot;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteAsync(CreateWalletAppDto appDto)
        {
            _logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var baseWalletModel = _walletMapper.Map(appDto);

            WalletAggregate walletDomainEntity;
            try
            {
                walletDomainEntity = _domainEntityFactory.GenerateAggregate<CreateWalletModel, WalletAggregate>(baseWalletModel);
            }
            catch (DomainValidationException<WalletAggregate> e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult(OutcomeState.Failure, default, $" | {e.ClassName}: {e.Message}");
            }

            var walletModel = walletDomainEntity.ToWalletModel();

            try
            {
                return await _walletDepot.ExecuteAsync(walletModel);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult(OutcomeState.Failure, default, $" | Data access failed: {e.Message}");
            }
        }
    }
}