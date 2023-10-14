using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.DomainModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.Wallet;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots
{
    public interface ICreateWalletDepot : ICommandHandler<WalletModel> { }
}