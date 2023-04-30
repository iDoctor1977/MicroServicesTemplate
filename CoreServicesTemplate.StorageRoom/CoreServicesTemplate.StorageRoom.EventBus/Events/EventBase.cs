using System.Text.Json;
using CoreServicesTemplate.Shared.Core.Interfaces.IEvents;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CoreServicesTemplate.StorageRoom.EventBus.Events;

public class EventBase<TDto, TLog> : IEventBus<TDto> where TDto : class
{
    private readonly IConnection _connectionFactory;
    private readonly string _queueName;
    private readonly ILogger<TLog> _logger;
    private IModel _channel;

    public EventBase(IConnectionFactory connectionFactory, string queueName, ILogger<TLog> logger)
    {
        _queueName = queueName;
        _logger = logger;

        _connectionFactory = connectionFactory.CreateConnection();
    }

    // Publish/Subscribe
    public void Publish(TDto eventDto)
    {
        _logger.LogInformation("----- Handling integration event: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        using (_channel = _connectionFactory.CreateModel())
        {
            _channel.ExchangeDeclare(exchange: "wallet", type: ExchangeType.Direct);

            var body = JsonSerializer.SerializeToUtf8Bytes(eventDto);

            _channel.BasicPublish(
                exchange: "wallet",
                routingKey: string.Empty,
                basicProperties: null,
                body: body);
        }
    }

    // Work queues
    //public void Publish(TDto eventDto)
    //{
    //    _logger.LogInformation("----- Handling integration event: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

    //    using (_channel = _connectionFactory.CreateModel())
    //    {
    //        _channel.QueueDeclare(
    //            queue: _queueName,
    //            durable: true,
    //            exclusive: false,
    //            autoDelete: false,
    //            arguments: null);

    //        var body = JsonSerializer.SerializeToUtf8Bytes(eventDto);

    //        var properties = _channel.CreateBasicProperties();
    //        properties.Persistent = true;

    //        _channel.BasicPublish(
    //            exchange: string.Empty,
    //            routingKey: _queueName,
    //            basicProperties: properties,
    //            body: body);
    //    }
    //}

    public void Dispose()
    {
        _channel.Close();
    }
}