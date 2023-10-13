using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Bus.Common.Interfaces.IServices
{
    public interface IBusService
    {
        Task<OperationResult<ResponseEmailPropertiesApiDto>> GetEmailPropertiesAsync(Guid ownerGuid);
    }
}
