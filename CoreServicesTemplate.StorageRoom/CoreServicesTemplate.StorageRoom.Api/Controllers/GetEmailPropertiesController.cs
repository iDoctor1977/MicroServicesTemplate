using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using Microsoft.AspNetCore.Mvc;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.Wallet;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/storageroom/[controller]")]
    public class GetEmailPropertiesController : ControllerBase
    {
        private readonly IGetEmailPropertiesFeature _getEmailPropertiesFeature;
        private readonly IDefaultMapper<ResponseEmailPropertiesApiDto, EmailPropertiesAppDto> _walletEventMapper;
        private readonly ILogger<CreateWalletController> _logger;

        public GetEmailPropertiesController(
            IGetEmailPropertiesFeature getEmailPropertiesFeature, 
            IDefaultMapper<ResponseEmailPropertiesApiDto, EmailPropertiesAppDto> walletEventMapper, 
            ILogger<CreateWalletController> logger)
        {
            _getEmailPropertiesFeature = getEmailPropertiesFeature;
            _walletEventMapper = walletEventMapper;
            _logger = logger;
        }

        // GET api/storageroom/createwalletevent/{ownerGuid}
        [HttpGet("{ownerGuid}")]
        public async Task<ActionResult<ResponseEmailPropertiesApiDto>> Get(Guid ownerGuid)
        {
            _logger.LogInformation("----- GET on controller: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            if (ownerGuid.Equals(null) || ownerGuid == Guid.Empty)
            {
                var message = " | Owner guid is not valid.";

                return BadRequest(message);
            }

            var operationResult = await _getEmailPropertiesFeature.ExecuteAsync(ownerGuid);

            if (operationResult.State.Equals(OutcomeState.Success))
            {
                if (operationResult.Value != null)
                {
                    var apiDto = _walletEventMapper.Map(operationResult.Value);

                    return Ok(apiDto);
                }
            }

            return UnprocessableEntity(operationResult);
        }
    }
}
