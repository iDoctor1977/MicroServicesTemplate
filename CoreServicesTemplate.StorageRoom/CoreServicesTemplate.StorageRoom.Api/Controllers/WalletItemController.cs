using CoreServicesTemplate.Shared.Core.DtoModels.WalletItem;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/storageroom/[controller]")]
    public class WalletItemController : ControllerBase
    {
        private readonly IGetWalletItemsFeature _getWalletItemsFeature;
        private readonly IDefaultMapper<WalletItemApiDto, WalletItemAppDto> _walletItemsMapper;
        private readonly ILogger<WalletItemController> _logger;

        public WalletItemController(
            IGetWalletItemsFeature getWalletItemsFeature, 
            IDefaultMapper<WalletItemApiDto, WalletItemAppDto> walletItemsMapper, 
            ILogger<WalletItemController> logger)
        {
            _getWalletItemsFeature = getWalletItemsFeature;
            _walletItemsMapper = walletItemsMapper;
            _logger = logger;
        }

        [HttpGet("{ownerGuid}")]
        public async Task<ActionResult<ICollection<WalletItemApiDto>>> Get(Guid ownerGuid)
        {
            _logger.LogInformation("----- Get wallet items: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            if (ownerGuid.Equals(null) || ownerGuid == Guid.Empty)
            {
                var message = " | Owner guid is not valid.";

                return BadRequest(message);
            }

            var operationResult = await _getWalletItemsFeature.ExecuteAsync(ownerGuid);

            if (operationResult.State.Equals(OutcomeState.Success))
            {
                ICollection<WalletItemApiDto> walletItemsDto = new List<WalletItemApiDto>();
                if (operationResult.Value != null)
                {
                    foreach (var walletItemModel in operationResult.Value)
                    {
                        walletItemsDto.Add(_walletItemsMapper.Map(walletItemModel));
                    }

                    return Ok(walletItemsDto);
                }
            }

            return UnprocessableEntity(operationResult.Message);
        }
    }
}
