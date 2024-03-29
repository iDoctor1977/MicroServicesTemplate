﻿using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.AspNetCore.Mvc;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;

namespace CoreServicesTemplate.StorageRoom.Api.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("api/storageroom/[controller]")]
    public class GetEmailPropertiesController : ControllerBase
    {
        private readonly IGetEmailPropertiesFeature _getEmailPropertiesFeature;
        private readonly IDefaultMapper<ResponseStorageRoomEmailPropertiesApiDto, EmailPropertiesAppModel> _walletEventMapper;
        private readonly ILogger<CreateWalletController> _logger;

        public GetEmailPropertiesController(
            IGetEmailPropertiesFeature getEmailPropertiesFeature, 
            IDefaultMapper<ResponseStorageRoomEmailPropertiesApiDto, EmailPropertiesAppModel> walletEventMapper, 
            ILogger<CreateWalletController> logger)
        {
            _getEmailPropertiesFeature = getEmailPropertiesFeature;
            _walletEventMapper = walletEventMapper;
            _logger = logger;
        }

        // GET api/storageroom/createwalletevent/{ownerGuid}
        [HttpGet("{ownerGuid}")]
        public async Task<ActionResult<ResponseStorageRoomEmailPropertiesApiDto>> Get(Guid ownerGuid)
        {
            _logger.LogInformation("----- GET on controller: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            if (ownerGuid.Equals(null) || ownerGuid == Guid.Empty)
            {
                var message = " | Owner guid is not valid.";

                return BadRequest(message);
            }

            OperationResult<EmailPropertiesAppModel> operationResult;
            try
            {
                operationResult = await _getEmailPropertiesFeature.ExecuteAsync(ownerGuid);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);

                return ValidationProblem(e.Message);
            }


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
