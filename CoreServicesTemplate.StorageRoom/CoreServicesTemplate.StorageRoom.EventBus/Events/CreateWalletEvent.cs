using CoreServicesTemplate.Shared.Core.DtoEvents;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CoreServicesTemplate.StorageRoom.EventBus.Events
{
    public class CreateWalletEvent : EventBase<CreateWalletEventDto>
    {
        public CreateWalletEvent(IConnectionFactory connectionFactory, string exchangeName, ILogger<CreateWalletEvent> logger) : base(connectionFactory, exchangeName, logger) { }
    }
}
