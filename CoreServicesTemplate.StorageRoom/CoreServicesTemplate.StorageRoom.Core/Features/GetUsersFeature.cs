using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features
{
    public class GetUsersFeature : IGetUsersFeature
    {
        private readonly IGetUsersDepot _getUsersDepot;
        private readonly ILogger<GetUserFeature> _logger;

        public GetUsersFeature(IGetUsersDepot getUsersDepot, ILogger<GetUserFeature> logger)
        {
            _getUsersDepot = getUsersDepot;
            _logger = logger;
        }

        public async Task<OperationResult<UsersAppModel>> ExecuteAsync()
        {
            try
            {
                return await _getUsersDepot.ExecuteAsync();
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
                throw new ApplicationException("Data access failed!");
            }
        }
    }
}
