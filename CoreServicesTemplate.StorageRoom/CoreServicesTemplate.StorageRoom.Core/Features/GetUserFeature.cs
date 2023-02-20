using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUserFeature : IGetUserFeature
    {
        private readonly IGetUserDepot _getUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;
        private readonly ILogger<GetUserFeature> _logger;

        public GetUserFeature(
            IGetUserDepot getUserDepot, 
            ISubStepSupplier subStepSupplier, 
            ILogger<GetUserFeature> logger)
        {
            _getUserDepot = getUserDepot;
            _subStepSupplier = subStepSupplier;
            _logger = logger;
        }

        public async Task<OperationResult<UserAppModel>> ExecuteAsync(UserAppModel @in)
        {
            OperationResult<UserAppModel> operationResult;

            // execute interaction with repository if necessary
            try
            {
                operationResult = await _getUserDepot.ExecuteAsync(@in);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                throw new ApplicationException("Data access failed!");
            }

            // Do something on User aggregate if necessary

            // execute getUserFeature sub steps
            // this part is added only for features scalability 
            operationResult = _subStepSupplier.ExecuteGetAsync(operationResult.Value);

            return operationResult;
        }
    }
}
