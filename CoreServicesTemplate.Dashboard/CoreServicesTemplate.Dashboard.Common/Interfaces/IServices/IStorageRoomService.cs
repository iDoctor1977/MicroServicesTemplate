using CoreServicesTemplate.Shared.Core.DtoModels.Wallet;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Dashboard.Common.Interfaces.IServices
{
    public interface IStorageRoomService
    {
        Task<OperationResult<HttpResponseMessage>> CreateNewWalletAsync(CreateWalletApiDto apiModel);
        Task<OperationResult<WalletApiDto>> GetWalletAsync(Guid ownerGuid);
    }
}
