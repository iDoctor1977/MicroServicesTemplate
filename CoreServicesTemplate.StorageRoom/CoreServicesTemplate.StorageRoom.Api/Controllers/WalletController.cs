using CoreServicesTemplate.Shared.Core.DtoModels.Wallet;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/storageroom/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly ICreateNewWalletFeature _createNewWalletFeature;
        private readonly IGetTradingAvailableBalanceFeature _availableBalanceFeature;
        private readonly IDefaultMapper<CreateWalletApiDto, CreateNewWalletAppDto> _customMapper;
        private readonly ILogger<WalletController> _logger;

        public WalletController(
            ICreateNewWalletFeature createNewWalletFeature,
            IGetTradingAvailableBalanceFeature availableBalanceFeature,
            IDefaultMapper<CreateWalletApiDto, CreateNewWalletAppDto> customMapper, 
            ILogger<WalletController> logger)
        {
            _createNewWalletFeature = createNewWalletFeature;
            _customMapper = customMapper;
            _availableBalanceFeature = availableBalanceFeature;
            _logger = logger;
        }

        // POST api/storageroom/wallet/{dto}
        [HttpPost("{newWalletDto}")]
        public async Task<ActionResult> Post(CreateWalletApiDto newWalletDto)
        {
            _logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            if (!ModelState.IsValid)
            {
                var message = $" | {ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)}";

                return BadRequest(message);
            }

            var model = _customMapper.Map(newWalletDto);

            var operationResult = await _createNewWalletFeature.ExecuteAsync(model);

            if (operationResult.State.Equals(OutcomeState.Success))
            {
                return Ok();

                // alternative return value
                // return Created(new Uri("api/storageroom/wallet/..."), model);
            }

            return UnprocessableEntity(operationResult.Message);
        }

        // GET api/storageroom/wallet/{ownerGuid}
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

        // GET api/storageroom/wallet
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // PUT api/storageroom/wallet/5
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] string value)
        {
            return Task.CompletedTask;
        }

        // DELETE api/storageroom/wallet/5
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return Task.CompletedTask;
        }
    }
}
