using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Shared.Core.Results;

namespace CoreServicesTemplate.Dashboard.Common.Interfaces.IServices
{
    public interface IStorageRoomService
    {
        Task<OperationResult<HttpResponseMessage>> CreateNewWalletAsync(CreateWalletModel appModel);
        Task<OperationResult<WalletModel>> GetWalletAsync(Guid ownerGuid);
    }
}
