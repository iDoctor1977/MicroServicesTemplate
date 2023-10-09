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
    public class CreateWalletEventController : ControllerBase
    {
        private readonly ICreateWalletEventFeature _createWalletEventFeature;
        private readonly IDefaultMapper<CreateWalletEventAppDto, CreateWalletEventApiDto> _walletEventMapper;
        private readonly ILogger<CreateWalletController> _logger;

        public CreateWalletEventController(
            ICreateWalletEventFeature createWalletEventFeature, 
            IDefaultMapper<CreateWalletEventAppDto, CreateWalletEventApiDto> walletEventMapper, 
            ILogger<CreateWalletController> logger)
        {
            _createWalletEventFeature = createWalletEventFeature;
            _walletEventMapper = walletEventMapper;
            _logger = logger;
        }

        // GET api/storageroom/createwalletevent/{ownerGuid}
        [HttpGet("{ownerGuid}")]
        public async Task<ActionResult<CreateWalletEventApiDto>> Get(Guid ownerGuid)
        {
            _logger.LogInformation("----- GET on controller: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            if (ownerGuid.Equals(null) || ownerGuid == Guid.Empty)
            {
                var message = " | Owner guid is not valid.";

                return BadRequest(message);
            }

            var operationResult = await _createWalletEventFeature.ExecuteAsync(ownerGuid);

            if (operationResult.State.Equals(OutcomeState.Success))
            {
                var walletApiModel = _walletEventMapper.Map(operationResult.Value);

                return Ok(walletApiModel);
            }

            return UnprocessableEntity(operationResult);
        }
    }
}
