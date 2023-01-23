using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandlers;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Api.Controllers
{
    [ApiController]
    [Route("dashboard/api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IConsolidator<UsersApiModel, UsersAppModel> _userCustomConsolidators;
        private readonly IQueryHandlerFeature<UsersAppModel> _getUsersFeature;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IQueryHandlerFeature<UsersAppModel> getUsersFeature,
            IConsolidator<UsersApiModel, UsersAppModel> userCustomConsolidators,
            ILogger<UserController> logger)
        {
            _logger = logger;
            _userCustomConsolidators = userCustomConsolidators;
            _getUsersFeature = getUsersFeature;
        }

        [HttpGet]
        public async Task<ActionResult<UsersApiModel>> GetAll()
        {
            var model = await _getUsersFeature.HandleAsync();

            var apiModel = _userCustomConsolidators.ToDataReverse(model).Resolve();

            return apiModel == null ? NotFound() : Ok(apiModel);
        }
    }
}
