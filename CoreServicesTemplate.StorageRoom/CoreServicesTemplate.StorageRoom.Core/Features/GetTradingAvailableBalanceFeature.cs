using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;
using CoreServicesTemplate.StorageRoom.Core.Domain.Exceptions;
using CoreServicesTemplate.StorageRoom.Core.Domain.SeedWork;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetTradingAvailableBalanceFeature : IGetTradingAvailableBalanceFeature
    {
        private readonly IDomainFactory _domainEntityFactory;
        private readonly IGetTradingAvailableBalanceDepot _walletDepot;
        private readonly ILogger<CreateWalletFeature> _logger;

        public GetTradingAvailableBalanceFeature(
            IDomainFactory domainEntityFactory,
            IGetTradingAvailableBalanceDepot walletDepot,
            ILogger<CreateWalletFeature> logger)
        {
            _domainEntityFactory = domainEntityFactory;
            _walletDepot = walletDepot;
            _logger = logger;
        }

        public async Task<OperationResult<decimal>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Get trading available balance: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            WalletModel? walletModel;
            decimal tradingAllowed;

            try
            {
                OperationResult<WalletModel>? result = await _walletDepot.ExecuteAsync(ownerGuid);
                walletModel = result.Value;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult<decimal>(OutcomeState.Failure, default, $" | Data access failed: {e.Message}");
            }

            try
            {
                if (walletModel != null)
                {
                    var walletDomainEntity = _domainEntityFactory.GenerateAggregate<WalletModel, WalletAggregate>(walletModel);
                    tradingAllowed = walletDomainEntity.CalculateTradingAvailableBalance();

                    return new OperationResult<decimal>(OutcomeState.Success, tradingAllowed);
                }
            }
            catch (DomainValidationException<WalletAggregate> e)
            {
                OperationResult<decimal> innerResult = null;
                if (e.InnerException != null)
                {
                    _logger.LogCritical($" | {GetType().Name}: {e.Message}");
                    _logger.LogCritical($" | {e.InnerException.GetType().Name}: {e.InnerException.Message}");

                    innerResult = new OperationResult<decimal>(OutcomeState.Failure, default, $" | {e.InnerException.GetType().Name}: {e.InnerException.Message}");
                }

                return new OperationResult<decimal>(OutcomeState.Failure, default, $" | {e.ClassName}: {e.Message}", innerResult);
            }

            return new OperationResult<decimal>(" | Data values is not valid.");
        }
    }
}
