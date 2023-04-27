using System.Text;
using System.Text.Json;
using CoreServicesTemplate.Shared.Core.Interfaces.IEvents;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CoreServicesTemplate.StorageRoom.EventBus.Events;

public class EventBase<TDto, TLog> : IEventBus<TDto> where TDto : class
{
    private readonly IConnection _connection;
    private readonly string _queueName;
    private readonly ILogger<TLog> _logger;

    private IModel Channel { get; set; }

    public EventBase(ConnectionFactory connectionFactory, string queueName, ILogger<TLog> logger)
    {
        _queueName = queueName;
        _logger = logger;

        _connection = connectionFactory.CreateConnection();
    }

    public async void PublishAsync(TDto eventDto)
    {
        _logger.LogInformation("----- Handling integration event: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        using (Channel = _connection.CreateModel())
        {
            Channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var payloadDto = JsonSerializer.Serialize(eventDto);
            var body = Encoding.UTF8.GetBytes(payloadDto);

            var properties = Channel.CreateBasicProperties();
            properties.Persistent = true;

            Channel.BasicPublish(
                exchange: string.Empty,
                routingKey: _queueName,
                basicProperties: properties,
                body: body);
        }
    }

    public void Dispose()
    {
        Channel.Close();
        _connection.Close();
    }
}