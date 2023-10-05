using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CoreServicesTemplate.StorageRoom.EventBus.Events
{
    public class CreateWalletEvent : EventBase<Shared.Core.EventModels.Wallet.CreateWalletEventDto>
    {
        public CreateWalletEvent(IConnectionFactory connectionFactory, string exchangeName, ILogger<CreateWalletEvent> logger) : base(connectionFactory, exchangeName, logger) { }
    }
}
