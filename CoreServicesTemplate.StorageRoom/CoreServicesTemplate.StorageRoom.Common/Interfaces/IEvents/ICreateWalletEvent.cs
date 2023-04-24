using CoreServicesTemplate.Shared.Core.DtoEvents;
using CoreServicesTemplate.Shared.Core.Interfaces.IEvents;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IEvents;

public interface ICreateWalletEvent : IEventBus<CreateWalletEventDto> { }