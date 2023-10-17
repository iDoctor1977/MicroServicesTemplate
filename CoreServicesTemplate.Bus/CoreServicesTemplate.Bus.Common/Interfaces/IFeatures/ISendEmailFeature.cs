using CoreServicesTemplate.Shared.Core.BusModels.Wallet;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.Bus.Common.Interfaces.IFeatures;

public interface ISendEmailFeature : ICommandHandler<WalletCreatedBusDto> { }