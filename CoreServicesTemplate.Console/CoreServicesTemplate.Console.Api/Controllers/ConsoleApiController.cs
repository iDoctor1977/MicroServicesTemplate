using System.Threading.Tasks;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Console.Api.Controllers
{
    [ApiController]
    [Route("ConsoleApi/[controller]")]
    public class ConsoleApiController : ControllerBase
    {
        private readonly ILogger<ConsoleApiController> _logger;

        private readonly IReadUsersFeature _readUsersFeature;

        public ConsoleApiController(IReadUsersFeature readUsersFeature, ILogger<ConsoleApiController> logger)
        {
            _logger = logger;

            _readUsersFeature = readUsersFeature;
        }

        [HttpGet]
        public async Task<UsersApiModel> Get()
        {
            var apiModel = await _readUsersFeature.HandleAsync();

            return apiModel;
        }
    }
}
