using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Exceptions;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Core.Domain.Aggregates;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetTradingAvailableBalanceFeature : IGetTradingAvailableBalanceFeature
    {
        private readonly IDomainEntityFactory _domainEntityFactory;
        private readonly IGetTradingAvailableBalanceDepot _walletDepot;
        private readonly ILogger<CreateWalletFeature> _logger;

        public GetTradingAvailableBalanceFeature(
            IDomainEntityFactory domainEntityFactory,
            IGetTradingAvailableBalanceDepot walletDepot,
            ILogger<CreateWalletFeature> logger)
        {
            _domainEntityFactory = domainEntityFactory;
            _walletDepot = walletDepot;
            _logger = logger;
        }

        public async Task<OperationResult<decimal>> ExecuteAsync(Guid ownerGuid)
        {
            _logger.LogInformation("----- Execute feature: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            WalletModel model;
            decimal tradingAllowed;

            try
            {
                OperationResult<WalletModel> result = await _walletDepot.ExecuteAsync(ownerGuid);
                model = result.Value;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult<decimal>(OutcomeState.Failure, default, $"Data access failed: {e.Message}");
            }

            try
            {
                var aggregate = _domainEntityFactory.Generate<WalletModel, WalletAggregate>(model);
                tradingAllowed = aggregate.CalculateTradingAvailableBalance();
            }
            catch (DomainValidationException<WalletAggregate> e)
            {
                OperationResult<decimal>? innerResult = null;
                if (e.InnerException != null)
                {
                    _logger.LogCritical($"{GetType().Name}: {e.Message}");
                    _logger.LogCritical($"{e.InnerException.GetType().Name}: {e.InnerException.Message}");

                    innerResult = new OperationResult<decimal>(OutcomeState.Failure, default, $"{e.InnerException.GetType().Name}: {e.InnerException.Message}");
                }

                return new OperationResult<decimal>(OutcomeState.Failure, default, $"{e.ClassName}: {e.Message}", innerResult);
            }

            return new OperationResult<decimal>(OutcomeState.Success, tradingAllowed);
        }
    }
}
