using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;
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
            // execute interaction with repository if necessary
            try
            {
                // execute getUserFeature sub steps
                // this part is added only for features scalability 
                _subStepSupplier.ExecuteGetAsync(@in);

                return await _getUserDepot.ExecuteAsync(@in);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult<UserAppModel>("Data access failed: " + e.Message);
            }
        }
    }
}
