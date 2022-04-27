using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Console.Api.Controllers
{
    [ApiController]
    [Route("ConsoleApi/[controller]")]
    public class ConsoleApiController : ControllerBase
    {
        private readonly ILogger<ConsoleApiController> _logger;

        private readonly IReadUsersFeature _readUsersFeature;

        public ConsoleApiController(IServiceProvider service, ILogger<ConsoleApiController> logger)
        {
            _logger = logger;

            _readUsersFeature = service.GetRequiredService<IReadUsersFeature>();
        }

        [HttpGet]
        public async Task<IEnumerable<UserApiModel>> Get()
        {
            var apiModels = await _readUsersFeature.ExecuteAsync(null);

            return apiModels;
        }
    }
}
