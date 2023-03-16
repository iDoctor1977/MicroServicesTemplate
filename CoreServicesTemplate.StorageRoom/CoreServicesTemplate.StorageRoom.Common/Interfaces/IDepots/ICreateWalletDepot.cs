using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots
{
    public interface ICreateWalletDepot : ICqrsCommand<WalletModel> { }
}