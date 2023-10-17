using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.Wallet;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/storageroom/[controller]")]
    public class CreateWalletController : ControllerBase
    {
        private readonly ICreateWalletFeature _createWalletFeature;
        private readonly IDefaultMapper<RequestCreateWalletApiDto, CreateWalletAppModel> _customMapper;
        private readonly ILogger<CreateWalletController> _logger;

        public CreateWalletController(
            ICreateWalletFeature createWalletFeature,
            IDefaultMapper<RequestCreateWalletApiDto, CreateWalletAppModel> customMapper,
            ILogger<CreateWalletController> logger)
        {
            _createWalletFeature = createWalletFeature;
            _customMapper = customMapper;
            _logger = logger;
        }

        // POST api/storageroom/createwallet/{apiDto}
        [HttpPost]
        public async Task<ActionResult> Post(RequestCreateWalletApiDto apiDto)
        {
            _logger.LogInformation("----- POST on controller: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            if (!ModelState.IsValid)
            {
                var message = $" | {ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)}";

                return BadRequest(message);
            }

            var appModel = _customMapper.Map(apiDto);

            var operationResult = await _createWalletFeature.ExecuteAsync(appModel);

            if (operationResult.State == OutcomeState.Success)
            {
                return Ok();
            }

            return UnprocessableEntity(operationResult);
        }
    }
}
