using CoreServicesTemplate.Event.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Event.Common.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.EventModels.Wallet;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;
using System.Net.Mail;

namespace CoreServicesTemplate.Event.Core.Features;

public class SendEmailEventFeature : ISendEmailEventFeature
{
    private readonly IEventService _eventService;
    private readonly ILogger<SendEmailEventFeature> _logger;

    public SendEmailEventFeature(IEventService eventService, ILogger<SendEmailEventFeature> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task<OperationResult> ExecuteAsync(CreateWalletEventDto model)
    {
        _logger.LogInformation($"---- Sending confirmation email #[{model.OwnerGuid}, {model.IsCreated}].");

        CreateWalletEventApiDto? eventModel;
        try
        {
            OperationResult<CreateWalletEventApiDto?> result = await _eventService.CreateWalletEventAsync(model.OwnerGuid);
            eventModel = result.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);

            return new OperationResult(OutcomeState.Failure, default, $"Data access failed: {e.Message}");

        }

        //simulated sending email
        await Task.Delay(1000);

        MailMessage mailMessage = new MailMessage(eventModel.FromAddress, eventModel.ToAddress);
        mailMessage.Subject = "Test new wallet event email";
        mailMessage.Body = $"This is for mail testing: #[{eventModel.Name} {eventModel.Surname}, {eventModel.Address}, {eventModel.Cap}, {eventModel.OwnerGuid}].";
        mailMessage.IsBodyHtml = true;

        _logger.LogInformation($"---- #{model.OwnerGuid} confirmation email sent.");

        return new OperationResult();
    }
}