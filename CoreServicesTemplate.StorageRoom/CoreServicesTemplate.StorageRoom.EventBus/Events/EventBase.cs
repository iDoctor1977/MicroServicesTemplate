using System.Text.Json;
using CoreServicesTemplate.Shared.Core.Interfaces.IEvents;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CoreServicesTemplate.StorageRoom.EventBus.Events;

public class EventBase<TDto, TLog> : IEventBus<TDto> where TDto : class
{
    private readonly IConnection _connectionFactory;
    private readonly string _exchangeName;
    private readonly ILogger<TLog> _logger;
    private IModel _channel;

    public EventBase(IConnectionFactory connectionFactory, string exchangeName, ILogger<TLog> logger)
    {
        _exchangeName = exchangeName;
        _logger = logger;

        _connectionFactory = connectionFactory.CreateConnection();
    }

    // Publish/Subscribe
    public void Publish(TDto eventDto)
    {
        _logger.LogInformation("----- Handling integration event: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        using (_channel = _connectionFactory.CreateModel())
        {
            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);

            var body = JsonSerializer.SerializeToUtf8Bytes(eventDto);

            _channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: string.Empty,
                basicProperties: null,
                body: body);
        }
    }

    public void Dispose()
    {
        _channel.Close();
    }
}