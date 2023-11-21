using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Exceptions;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.DomainModels.Wallet;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features;

public class GetEmailPropertiesFeature : IGetEmailPropertiesFeature
{
    private readonly IDefaultMapper<EmailPropertiesAppModel, EmailPropertiesModel> _defaultMapper;
    private readonly IGetEmailPropertiesEfDepot _walletEventEfDepot;
    private readonly ILogger<GetEmailPropertiesFeature> _logger;

    public GetEmailPropertiesFeature(
        IDefaultMapper<EmailPropertiesAppModel, EmailPropertiesModel> defaultMapper,
        IGetEmailPropertiesEfDepot walletEventEfDepot,
        ILogger<GetEmailPropertiesFeature> logger)
    {
        _defaultMapper = defaultMapper;
        _walletEventEfDepot = walletEventEfDepot;
        _logger = logger;
    }

    public async Task<OperationResult<EmailPropertiesAppModel>> ExecuteAsync(Guid ownerGuid)
    {
        _logger.LogInformation("----- Execute feature: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        EmailPropertiesModel model;
        try
        {
            var result = await _walletEventEfDepot.ExecuteAsync(ownerGuid);
            model = result.Value ?? throw new FeatureValidationException<GetEmailPropertiesFeature>($"{GetType().Name}: {nameof(result.Value)} is null");
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);

            return new OperationResult<EmailPropertiesAppModel>(OutcomeState.Failure, default, $"Data access failed: {e.Message}");
        }

        var appModel = _defaultMapper.Map(model);

        return new OperationResult<EmailPropertiesAppModel>(OutcomeState.Success, appModel);
    }
}