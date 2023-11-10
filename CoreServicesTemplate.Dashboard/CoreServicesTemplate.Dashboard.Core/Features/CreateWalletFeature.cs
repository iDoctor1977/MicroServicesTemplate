using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IServices;
using CoreServicesTemplate.Dashboard.Common.Models.AppModels.Wallets;
using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;
using CoreServicesTemplate.Dashboard.Core.Domain;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Exceptions;
using CoreServicesTemplate.Shared.Core.Interfaces.IFactories;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Dashboard.Core.Features
{
    public class CreateWalletFeature : ICreateWalletFeature
    {
        private readonly IDomainEntityFactory _domainEntityFactory;
        private readonly ICreateWalletService _createWalletService;
        private readonly IDefaultMapper<CreateWalletAppModel, CreateWalletModel> _mapper;
        private readonly ILogger<CreateWalletFeature> _logger;

        public CreateWalletFeature(
            IDomainEntityFactory domainEntityFactory,
            ICreateWalletService createWalletService,
            IDefaultMapper<CreateWalletAppModel, CreateWalletModel> mapper,
            ILogger<CreateWalletFeature> logger) 
        {
            _domainEntityFactory = domainEntityFactory;
            _createWalletService = createWalletService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OperationResult> ExecuteAsync(CreateWalletAppModel appModel)
        {
            _logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            var baseModel = _mapper.Map(appModel);

            WalletAggregate aggregate;
            try
            {
                aggregate = _domainEntityFactory.Generate<CreateWalletModel, WalletAggregate>(baseModel);
            }
            catch (DomainValidationException<WalletAggregate> e)
            {
                _logger.LogCritical(e.Message);

                return new OperationResult(OutcomeState.Failure, default, $"{e.ClassName}: {e.Message}");
            }

            var model = aggregate.ToModel();

            var responseMessage = await _createWalletService.ExecuteAsync(model);

            return new OperationResult(OutcomeState.Success, responseMessage);
        }
    }
}
