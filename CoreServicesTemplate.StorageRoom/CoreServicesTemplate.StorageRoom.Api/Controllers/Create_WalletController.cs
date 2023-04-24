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
    public class Create_WalletController : ControllerBase
    {
        private readonly ICreateWalletFeature _createWalletFeature;
        private readonly IDefaultMapper<CreateWalletApiDto, CreateNewWalletAppDto> _customMapper;
        private readonly ILogger<Create_WalletController> _logger;

        public Create_WalletController(
            ICreateWalletFeature createWalletFeature,
            IDefaultMapper<CreateWalletApiDto, CreateNewWalletAppDto> customMapper, 
            ILogger<Create_WalletController> logger)
        {
            _createWalletFeature = createWalletFeature;
            _customMapper = customMapper;
            _logger = logger;
        }

        // POST api/storageroom/create_wallet/{apiDto}
        [HttpPost("{apiDto}")]
        public async Task<ActionResult> Post(CreateWalletApiDto apiDto)
        {
            _logger.LogInformation("----- Create wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            if (!ModelState.IsValid)
            {
                var message = $" | {ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)}";

                return BadRequest(message);
            }

            var model = _customMapper.Map(apiDto);

            var operationResult = await _createWalletFeature.ExecuteAsync(model);

            if (operationResult.State.Equals(OutcomeState.Success))
            {
                return Ok();

                // alternative return value
                // return Created(new Uri("api/storageroom/wallet/..."), model);
            }

            return UnprocessableEntity(operationResult.Message);
        }
    }
}
