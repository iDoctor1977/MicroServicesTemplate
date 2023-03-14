using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;
using CoreServicesTemplate.StorageRoom.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUserFeature : IGetUserFeature
    {
        private readonly IDefaultMapper<UserAppModel, UserAggModel> _defaultMapper;
        private readonly IGetUserDepot _getUserDepot;
        private readonly ISubStepSupplier _subStepSupplier;
        private readonly ILogger<GetUserFeature> _logger;

        public GetUserFeature(
            IDefaultMapper<UserAppModel, UserAggModel> defaultMapper,
            IGetUserDepot getUserDepot, 
            ISubStepSupplier subStepSupplier, 
            ILogger<GetUserFeature> logger)
        {
            _getUserDepot = getUserDepot;
            _subStepSupplier = subStepSupplier;
            _logger = logger;
            _defaultMapper = defaultMapper;
        }

        public async Task<OperationResult<UserAppModel>> ExecuteAsync(UserAppModel @in)
        {
            // execute interaction with repository if necessary
            try
            {
                var aggModel = _defaultMapper.Map(@in);

                // execute getUserFeature sub steps
                // this part is added only for features scalability 
                _subStepSupplier.ExecuteGetAsync(@in);

                var result = await _getUserDepot.ExecuteAsync(aggModel);

                if (result.Value != null)
                {
                    var appModel = _defaultMapper.Map(result.Value);

                    return new OperationResult<UserAppModel>(appModel);
                }

                return new OperationResult<UserAppModel>("Data access failed");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                return new OperationResult<UserAppModel>($"Data access failed: {e.Message}");
            }
        }
    }
}
