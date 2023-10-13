using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.EventModels.Wallet;
using RabbitMQ.Client;

namespace CoreServicesTemplate.StorageRoom.Api.Bus
{
    public class WalletCreatedBus : BusBase<WalletCreatedBusDto>
    {
        public WalletCreatedBus(IConnectionFactory connectionFactory, string exchangeName, ILogger<WalletCreatedBus> logger) : base(connectionFactory, exchangeName, logger) { }
    }
}
