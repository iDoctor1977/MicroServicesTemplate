using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.EventModels.Wallet;
using RabbitMQ.Client;

namespace CoreServicesTemplate.StorageRoom.Api.Events
{
    public class CreateWalletEvent : EventBase<CreateWalletEventDto>
    {
        public CreateWalletEvent(IConnectionFactory connectionFactory, string exchangeName, ILogger<CreateWalletEvent> logger) : base(connectionFactory, exchangeName, logger) { }
    }
}
