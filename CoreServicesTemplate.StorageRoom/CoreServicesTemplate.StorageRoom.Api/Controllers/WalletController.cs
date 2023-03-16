using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [Route("api/storageroom/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly ICreateWalletFeature _createWalletFeature;
        private readonly IGetTradingAvailableBalanceFeature _availableBalanceFeature;
        private readonly IDefaultMapper<CreateWalletApiDto, CreateWalletAppDto> _customMapper;
        private readonly ILogger<WalletController> _logger;

        public WalletController(
            ICreateWalletFeature createWalletFeature,
            IGetTradingAvailableBalanceFeature availableBalanceFeature,
            IDefaultMapper<CreateWalletApiDto, CreateWalletAppDto> customMapper, 
            ILogger<WalletController> logger)
        {
            _createWalletFeature = createWalletFeature;
            _customMapper = customMapper;
            _availableBalanceFeature = availableBalanceFeature;
            _logger = logger;
        }

        [HttpPost("{walletDto}")]
        public async Task<ActionResult> Create(CreateWalletApiDto walletDto)
        {
            _logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            if (!ModelState.IsValid)
            {
                var message = $" | {ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)}";

                return BadRequest(message);
            }

            var model = _customMapper.Map(walletDto);

            var operationResult = await _createWalletFeature.ExecuteAsync(model);

            if (operationResult.State.Equals(OutcomeState.Success))
            {
                return Ok();

                // alternative return value
                // return Created(new Uri("api/storageroom/wallet/..."), model);
            }

            return UnprocessableEntity(operationResult.Message);
        }

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
