﻿using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Api.Controllers
{
    [ApiController]
    [Route("dashboard/api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IConsolidator<UsersApiModel, UsersAppModel> _usersCustomConsolidators;
        private readonly IGetUsersFeature _getUsersFeature;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IGetUsersFeature getUsersFeature,
            IConsolidator<UsersApiModel, UsersAppModel> usersCustomConsolidators,
            ILogger<UserController> logger)
        {
            _logger = logger;
            _usersCustomConsolidators = usersCustomConsolidators;
            _getUsersFeature = getUsersFeature;
        }

        [HttpGet]
        public async Task<ActionResult<UsersApiModel>> GetAll()
        {
            var model = await _getUsersFeature.HandleAsync();

            var apiModel = _usersCustomConsolidators.ToDataReverse(model).Resolve();

            return apiModel == null ? NotFound() : Ok(apiModel);
        }
    }
}
