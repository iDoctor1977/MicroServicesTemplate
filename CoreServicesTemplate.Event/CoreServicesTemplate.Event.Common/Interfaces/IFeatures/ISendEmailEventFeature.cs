using CoreServicesTemplate.Shared.Core.EventModels.Wallet;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.Event.Common.Interfaces.IFeatures;

public interface ISendEmailEventFeature : ICommandHandler<CreateWalletEventDto> { }