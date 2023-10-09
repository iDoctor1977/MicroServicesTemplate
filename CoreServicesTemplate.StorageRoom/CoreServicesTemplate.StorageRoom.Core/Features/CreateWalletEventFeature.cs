using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures;
using CoreServicesTemplate.StorageRoom.Common.Models.Wallet;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.StorageRoom.Core.Features;

public class CreateWalletEventFeature : ICreateWalletEventFeature
{
    private readonly IDefaultMapper<CreateWalletEventAppDto, CreateWalletEventModel> _defaultMapper;
    private readonly ICreateWalletEventEfDepot _walletEventEfDepot;
    private readonly ILogger<CreateWalletEventFeature> _logger;

    public CreateWalletEventFeature(
        IDefaultMapper<CreateWalletEventAppDto, CreateWalletEventModel> defaultMapper,
        ICreateWalletEventEfDepot walletEventEfDepot,
        ILogger<CreateWalletEventFeature> logger)
    {
        _defaultMapper = defaultMapper;
        _walletEventEfDepot = walletEventEfDepot;
        _logger = logger;
    }

    public async Task<OperationResult<CreateWalletEventAppDto>> ExecuteAsync(Guid ownerGuid)
    {
        _logger.LogInformation("----- Execute feature: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        CreateWalletEventModel walletEventModel;
        try
        {
            var result = await _walletEventEfDepot.ExecuteAsync(ownerGuid);
            walletEventModel = result.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);

            return new OperationResult<CreateWalletEventAppDto>(OutcomeState.Failure, default, $"Data access failed: {e.Message}");
        }

        var walletEventAppDto = _defaultMapper.Map(walletEventModel);

        return new OperationResult<CreateWalletEventAppDto>(OutcomeState.Success, walletEventAppDto);
    }
}