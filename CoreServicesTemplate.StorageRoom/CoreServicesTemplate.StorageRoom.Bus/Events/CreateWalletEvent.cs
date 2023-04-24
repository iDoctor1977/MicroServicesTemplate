using System.Text;
using System.Text.Json;
using CoreServicesTemplate.Shared.Core.DtoEvents;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IEvents;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CoreServicesTemplate.StorageRoom.Bus.Events
{
    public class CreateWalletEvent : ICreateWalletEvent
    {
        private readonly ConnectionFactory _factory;
        private readonly string _queueName;
        private readonly ILogger<CreateWalletEvent> _logger;

        private IModel Channel { get; set; }

        public CreateWalletEvent(ConnectionFactory factory, string queueName, ILogger<CreateWalletEvent> logger)
        {
            _factory = factory;
            _queueName = queueName;
            _logger = logger;
        }

        public async Task PublishAsync(CreateWalletEventDto eventDto)
        {
            _logger.LogInformation("----- Handling integration event: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

            using var connection = _factory.CreateConnection();

            using (Channel = connection.CreateModel())
            {
                Channel.QueueDeclare(
                    queue: _queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var payload = JsonSerializer.Serialize(eventDto);
                var body = Encoding.UTF8.GetBytes(payload);

                var properties = Channel.CreateBasicProperties();
                properties.Persistent = true;

                Channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: _queueName,
                    basicProperties: properties,
                    body: body);
            }

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            Channel.Dispose();
        }
    }
}
