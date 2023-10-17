using CoreServicesTemplate.Bus.Common.Models;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Bus.Common.Interfaces.IServices
{
    public interface IBusService
    {
        Task<OperationResult<EmailPropertiesModel>> GetEmailPropertiesAsync(Guid ownerGuid);
    }
}
