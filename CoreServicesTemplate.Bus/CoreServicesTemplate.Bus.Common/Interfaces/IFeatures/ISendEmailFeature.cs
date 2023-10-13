using CoreServicesTemplate.Shared.Core.EventModels.Wallet;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.Bus.Common.Interfaces.IFeatures;

public interface ISendEmailFeature : ICommandHandler<WalletCreatedBusDto> { }