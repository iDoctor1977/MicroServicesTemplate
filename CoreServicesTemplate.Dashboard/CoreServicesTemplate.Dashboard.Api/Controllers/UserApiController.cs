﻿using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.Dashboard.Api.Controllers
{
    [ApiController]
    [Route("Dashboard/[controller]/[action]")]
    public class UserApiController : ControllerBase
    {
        private readonly IConsolidator<UsersApiModel, UsersAppModel> _userCustomConsolidators;
        private readonly IFeatureQuery<UsersAppModel> _getUsersFeature;
        private readonly ILogger<UserApiController> _logger;

        public UserApiController(
            IFeatureQuery<UsersAppModel> getUsersFeature,
            IConsolidator<UsersApiModel, UsersAppModel> userCustomConsolidators,
            ILogger<UserApiController> logger)
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