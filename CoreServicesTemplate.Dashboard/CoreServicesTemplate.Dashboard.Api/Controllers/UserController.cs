using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Api.Controllers
{
    [ApiController]
    [Route("api/dashboard/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IMapping<UsersApiModel, UsersAppModel> _usersCustomMapper;
        private readonly IGetUsersFeature _getUsersFeature;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IGetUsersFeature getUsersFeature,
            IMapping<UsersApiModel, UsersAppModel> usersCustomMapper,
            ILogger<UserController> logger)
        {
            _logger = logger;
            _usersCustomMapper = usersCustomMapper;
            _getUsersFeature = getUsersFeature;
        }

        [HttpGet]
        public async Task<ActionResult<UsersApiModel>> GetAll()
        {
            var model = await _getUsersFeature.HandleAsync();

            var apiModel = _usersCustomMapper.Map(model);

            return apiModel == null ? NotFound() : Ok(apiModel);
        }
    }
}
