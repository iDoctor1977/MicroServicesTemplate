﻿using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Console.Api.Controllers
{
    [ApiController]
    [Route("ConsoleApi/[controller]")]
    public class ConsoleApiController : ControllerBase
    {
        private readonly IConsolidators<UsersModel, UsersApiModel> _consolidators;
        private readonly IGetUsersFeature _getUsersFeature;
        private readonly ILogger<ConsoleApiController> _logger;

        public ConsoleApiController(
            IGetUsersFeature getUsersFeature,
            IConsolidators<UsersModel, UsersApiModel> consolidators,
            ILogger<ConsoleApiController> logger)
        {
            _logger = logger;
            _consolidators = consolidators;

            _getUsersFeature = getUsersFeature;
        }

        [HttpGet]
        public async Task<UsersApiModel> Get()
        {
            var model = await _getUsersFeature.HandleAsync();

            var apiModel = _consolidators.ToData(model);

            return apiModel;
        }
    }
}
