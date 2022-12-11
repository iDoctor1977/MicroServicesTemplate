using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Api.Controllers
{
    [ApiController]
    [Route("Dashboard/[controller]/[action]")]
    public class HomeApiController : ControllerBase
    {
        private readonly IConsolidators<UsersModel, UsersApiModel> _consolidators;
        private readonly IGetUsersFeature _getUsersFeature;
        private readonly ILogger<HomeApiController> _logger;

        public HomeApiController(
            IGetUsersFeature getUsersFeature,
            IConsolidators<UsersModel, UsersApiModel> consolidators,
            ILogger<HomeApiController> logger)
        {
            _logger = logger;
            _consolidators = consolidators;
            _getUsersFeature = getUsersFeature;
        }

        [HttpGet]
        public async Task<UsersApiModel> GetAll()
        {
            var model = await _getUsersFeature.HandleAsync();

            var apiModel = _consolidators.ToData(model);

            return apiModel;
        }
    }
}
