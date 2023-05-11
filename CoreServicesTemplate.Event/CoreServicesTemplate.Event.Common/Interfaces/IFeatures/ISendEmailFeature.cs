using CoreServicesTemplate.Shared.Core.DtoEvents;
using CoreServicesTemplate.Shared.Core.Interfaces.IHandlers;

namespace CoreServicesTemplate.Event.Handler.Workers;

public interface ISendEmailFeature : ICommandHandler<CreateWalletEventDto> { }