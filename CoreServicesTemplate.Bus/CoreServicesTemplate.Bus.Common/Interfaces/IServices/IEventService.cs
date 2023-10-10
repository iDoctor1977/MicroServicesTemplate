using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Bus.Common.Interfaces.IServices
{
    public interface IEventService
    {
        Task<OperationResult<CreateWalletEventApiDto?>> CreateWalletEventAsync(Guid ownerGuid);
    }
}
