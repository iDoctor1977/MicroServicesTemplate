using System.Net.Mail;
using CoreServicesTemplate.Bus.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Bus.Common.Interfaces.IServices;
using CoreServicesTemplate.Bus.Common.Models;
using CoreServicesTemplate.Shared.Core.BusModels.Wallet;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Bus.Core.Features;

public class SendEmailFeature : ISendEmailFeature
{
    private readonly IBusService _eventService;
    private readonly ILogger<SendEmailFeature> _logger;

    public SendEmailFeature(
        IBusService eventService,
        ILogger<SendEmailFeature> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task<OperationResult> ExecuteAsync(WalletCreatedBusDto busDto)
    {
        _logger.LogInformation($"---- Sending confirmation email #[{busDto.OwnerGuid}, {busDto.IsCreated}].");

        EmailPropertiesModel model;
        try
        {
            OperationResult<EmailPropertiesModel> result = await _eventService.GetEmailPropertiesAsync(busDto.OwnerGuid);
            model = result.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);

            return new OperationResult(OutcomeState.Failure, default, $"Data access failed: {e.Message}");

        }

        //simulated sending email
        await Task.Delay(1000);

        MailMessage mailMessage = new MailMessage(model.FromAddress, model.ToAddress);
        mailMessage.Subject = "Test new wallet event email";
        mailMessage.Body = $"This is for mail testing: #[{model.Name} {model.Surname}, {model.Address}, {model.Cap}, {model.OwnerGuid}].";
        mailMessage.IsBodyHtml = true;

        _logger.LogInformation($"---- #{busDto.OwnerGuid} confirmation email sent.");

        return new OperationResult();
    }
}