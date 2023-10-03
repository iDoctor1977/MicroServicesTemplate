using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/storageroom/[controller]")]
    public class GetTradingAvailableBalanceController : ControllerBase
    {
        private readonly IGetTradingAvailableBalanceFeature _availableBalanceFeature;
        private readonly ILogger<GetTradingAvailableBalanceController> _logger;

        public GetTradingAvailableBalanceController(
            IGetTradingAvailableBalanceFeature availableBalanceFeature,
            ILogger<GetTradingAvailableBalanceController> logger)
        {
            _availableBalanceFeature = availableBalanceFeature;
            _logger = logger;
        }

        // GET api/storageroom/gettradingavailablebalance/{ownerGuid}
        [HttpGet("{ownerGuid}")]
        public async Task<ActionResult<decimal>> Get(Guid ownerGuid)
        {
            _logger.LogInformation("----- GET on controller: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            if (ownerGuid.Equals(null) || ownerGuid == Guid.Empty)
            {
                var message = " | Owner guid is not valid.";

                return BadRequest(message);
            }

            var operationResult = await _availableBalanceFeature.ExecuteAsync(ownerGuid);

            if (operationResult.State.Equals(OutcomeState.Success))
            {
                if (operationResult.Value != decimal.Zero)
                {
                    return Ok(operationResult.Value);
                }
            }

            return UnprocessableEntity(operationResult);
        }
    }
}
