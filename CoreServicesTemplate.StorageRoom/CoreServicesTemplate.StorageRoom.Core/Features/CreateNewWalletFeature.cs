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
    public class CreateNewWalletFeature : ICreateNewWalletFeature
    {
        private readonly IDomainEntityFactory _domainEntityEntityFactory;
        private readonly IDefaultMapper<CreateNewWalletAppDto, CreateWalletModel> _walletMapper;
        private readonly ICreateNewWalletDepot _walletDepot;
        private readonly ILogger<CreateNewWalletFeature> _logger;

        public CreateNewWalletFeature(
            IDomainEntityFactory domainEntityEntityFactory,
            IDefaultMapper<CreateNewWalletAppDto, CreateWalletModel> walletMapper,
            ICreateNewWalletDepot walletDepot, ILogger<CreateNewWalletFeature> logger)
        {
            _domainEntityEntityFactory = domainEntityEntityFactory;
            _walletMapper = walletMapper;
            _walletDepot = walletDepot;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteAsync(CreateNewWalletAppDto appDto)
        {
            _logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var baseWalletModel = _walletMapper.Map(appDto);

            WalletAggregate walletDomainEntity;
            try
            {
                walletDomainEntity = _domainEntityEntityFactory.GenerateAggregate<CreateWalletModel, WalletAggregate>(baseWalletModel);
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