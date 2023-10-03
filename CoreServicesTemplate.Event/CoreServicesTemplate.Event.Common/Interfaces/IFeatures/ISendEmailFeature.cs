using CoreServicesTemplate.Shared.Core.Events;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.Event.Common.Interfaces.IFeatures;

public interface ISendEmailFeature : ICommandHandler<CreateWalletEventDto> { }