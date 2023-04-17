using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/storageroom/[controller]")]
    public class Get_Trading_Available_BalanceController : ControllerBase
    {
        private readonly IGetTradingAvailableBalanceFeature _availableBalanceFeature;
        private readonly ILogger<Get_Trading_Available_BalanceController> _logger;

        public Get_Trading_Available_BalanceController(
            IGetTradingAvailableBalanceFeature availableBalanceFeature,
            ILogger<Get_Trading_Available_BalanceController> logger)
        {
            _availableBalanceFeature = availableBalanceFeature;
            _logger = logger;
        }

        // GET api/storageroom/get_trading_available_balance/{ownerGuid}
        [HttpGet("{ownerGuid}")]
        public async Task<ActionResult<decimal>> Get(Guid ownerGuid)
        {
            _logger.LogInformation("----- Get trading available balance: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

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

            return UnprocessableEntity(operationResult.Message);
        }
    }
}
