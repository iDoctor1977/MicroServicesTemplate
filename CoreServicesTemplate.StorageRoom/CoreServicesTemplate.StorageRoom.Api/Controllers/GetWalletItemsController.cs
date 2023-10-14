using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.WalletItem;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.WalletItem;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/storageroom/[controller]")]
    public class GetWalletItemsController : ControllerBase
    {
        private readonly IGetWalletItemsFeature _getWalletItemsFeature;
        private readonly IDefaultMapper<MarketItemApiDto, WalletItemAppModel> _walletItemsMapper;
        private readonly ILogger<GetWalletItemsController> _logger;

        public GetWalletItemsController(
            IGetWalletItemsFeature getWalletItemsFeature, 
            IDefaultMapper<MarketItemApiDto, WalletItemAppModel> walletItemsMapper, 
            ILogger<GetWalletItemsController> logger)
        {
            _getWalletItemsFeature = getWalletItemsFeature;
            _walletItemsMapper = walletItemsMapper;
            _logger = logger;
        }

        // GET api/storageroom/getwalletitems/{ownerGuid}
        [HttpGet("{ownerGuid}")]
        public async Task<ActionResult<ICollection<MarketItemApiDto>>> Get(Guid ownerGuid)
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
                ICollection<MarketItemApiDto> apiDtos = new List<MarketItemApiDto>();
                if (operationResult.Value != null)
                {
                    foreach (var walletItemModel in operationResult.Value)
                    {
                        apiDtos.Add(_walletItemsMapper.Map(walletItemModel));
                    }

                    return Ok(apiDtos);
                }
            }

            return UnprocessableEntity(operationResult.Message);
        }
    }
}
