using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUsersFeature : IGetUsersFeature
    {
        private readonly IDefaultMapper<UserAppModel, UserAggModel> _defaultMapper;
        private readonly IGetUsersDepot _getUsersDepot;
        private readonly ILogger<GetUserFeature> _logger;

        public GetUsersFeature(
            IDefaultMapper<UserAppModel, UserAggModel> defaultMapper,
            IGetUsersDepot getUsersDepot, 
            ILogger<GetUserFeature> logger)
        {
            _getUsersDepot = getUsersDepot;
            _logger = logger;
            _defaultMapper = defaultMapper;
        }

        public async Task<OperationResult<ICollection<UserAppModel>>> ExecuteAsync()
        {
            try
            {
                var result = await _getUsersDepot.ExecuteAsync();

                if (result.Value != null)
                {
                    var appModels = new List<UserAppModel>(_defaultMapper.Map(result.Value));
                    return new OperationResult<ICollection<UserAppModel>>(appModels);
                }

                return new OperationResult<ICollection<UserAppModel>>("Data access failed!");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                throw new ApplicationException("Data access failed!");
            }
        }
    }
}
