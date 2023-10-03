using CoreServicesTemplate.Event.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Shared.Core.Events;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Event.Core.Features;

public class SendEmailFeature : ISendEmailFeature
{
    private readonly ILogger<SendEmailFeature> _logger;

    public SendEmailFeature(ILogger<SendEmailFeature> logger)
    {
        _logger = logger;
    }

    public async Task<OperationResult> ExecuteAsync(CreateWalletEventDto model)
    {
        _logger.LogInformation($"---- Sending confirmation email #[{model.OwnerGuid}, {model.IsCreated}].");

        await Task.Delay(1000);

        _logger.LogInformation($"---- #{model.OwnerGuid} confirmation email sent.");

        return new OperationResult();
    }
}