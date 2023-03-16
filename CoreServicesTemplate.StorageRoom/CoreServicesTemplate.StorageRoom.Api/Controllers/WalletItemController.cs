using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.WalletItem;
using Microsoft.AspNetCore.Mvc;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletItemController : ControllerBase
    {
        private readonly IGetWalletItemsFeature _getWalletItemsFeature;
        private readonly IDefaultMapper<WalletItemApiModel, WalletItemAppDto> _walletItemsMapper;

        public WalletItemController(IGetWalletItemsFeature getWalletItemsFeature, IDefaultMapper<WalletItemApiModel, WalletItemAppDto> walletItemsMapper)
        {
            _getWalletItemsFeature = getWalletItemsFeature;
            _walletItemsMapper = walletItemsMapper;
        }

        [HttpGet("{ownerGuid}")]
        public async Task<ActionResult<ICollection<WalletItemApiModel>>> Get(Guid ownerGuid)
        {
            if (ownerGuid.Equals(null) || ownerGuid == Guid.Empty)
            {
                var message = " | Owner guid is not valid.";

                return BadRequest(message);
            }

            var operationResult = await _getWalletItemsFeature.ExecuteAsync(ownerGuid);

            if (operationResult.State.Equals(OutcomeState.Success))
            {
                ICollection<WalletItemApiModel> walletItemsDto = new List<WalletItemApiModel>();
                if (operationResult.Value != null)
                {
                    foreach (var walletItemModel in operationResult.Value)
                    {
                        walletItemsDto.Add(_walletItemsMapper.Map(walletItemModel));
                    }

                    return Ok(walletItemsDto);
                }
            }

            return UnprocessableEntity(operationResult);

        }
    }
}
