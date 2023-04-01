using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels.Wallet;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures
{
    public interface ICreateNewWalletFeature : ICommandHandler<CreateNewWalletAppDto> { }
}