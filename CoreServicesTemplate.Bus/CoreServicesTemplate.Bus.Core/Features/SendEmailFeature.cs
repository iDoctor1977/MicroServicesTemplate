using System.Net.Mail;
using CoreServicesTemplate.Bus.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Bus.Common.Interfaces.IServices;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.Shared.Core.EventModels.Wallet;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.Logging;

namespace CoreServicesTemplate.Bus.Core.Features;

public class SendEmailFeature : ISendEmailFeature
{
    private readonly IBusService _eventService;
    private readonly ILogger<SendEmailFeature> _logger;

    public SendEmailFeature(IBusService eventService, ILogger<SendEmailFeature> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    public async Task<OperationResult> ExecuteAsync(WalletCreatedBusDto busDto)
    {
        _logger.LogInformation($"---- Sending confirmation email #[{busDto.OwnerGuid}, {busDto.IsCreated}].");

        ResponseEmailPropertiesApiDto apiDto;
        try
        {
            OperationResult<ResponseEmailPropertiesApiDto> result = await _eventService.GetEmailPropertiesAsync(busDto.OwnerGuid);
            apiDto = result.Value;
        }
        catch (Exception e)
        {
            _logger.LogCritical(e.Message);

            return new OperationResult(OutcomeState.Failure, default, $"Data access failed: {e.Message}");

        }

        //simulated sending email
        await Task.Delay(1000);

        MailMessage mailMessage = new MailMessage(apiDto.FromAddress, apiDto.ToAddress);
        mailMessage.Subject = "Test new wallet event email";
        mailMessage.Body = $"This is for mail testing: #[{apiDto.Name} {apiDto.Surname}, {apiDto.Address}, {apiDto.Cap}, {apiDto.OwnerGuid}].";
        mailMessage.IsBodyHtml = true;

        _logger.LogInformation($"---- #{busDto.OwnerGuid} confirmation email sent.");

        return new OperationResult();
    }
}