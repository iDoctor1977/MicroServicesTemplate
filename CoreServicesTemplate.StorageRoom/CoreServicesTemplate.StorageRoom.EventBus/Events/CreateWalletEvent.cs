using CoreServicesTemplate.Shared.Core.DtoEvents;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CoreServicesTemplate.StorageRoom.EventBus.Events
{
    public class CreateWalletEvent : EventBase<CreateWalletEventDto, CreateWalletEvent>
    {
        public CreateWalletEvent(ConnectionFactory connectionFactory, string queueName, ILogger<CreateWalletEvent> logger) : base(connectionFactory, queueName, logger) { }
    }
}
