using System;
using System.Text.Json;
using CoreServicesTemplate.Shared.Core.Interfaces.IEvents;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CoreServicesTemplate.Shared.Core.Bases;

public class EventBase<TDto> : IEventBus<TDto> where TDto : class
{
    private readonly IConnection _connection;
    private readonly string _exchangeName;
    private readonly ILogger _logger;
    private IModel _channel;

    public EventBase(IConnectionFactory connectionFactory, string exchangeName, ILogger logger)
    {
        _exchangeName = exchangeName;
        _logger = logger;

        _connection = connectionFactory.CreateConnection();
    }

    // Publish/Subscribe
    public void Publish(TDto eventDto)
    {
        _logger.LogInformation("----- Handling integration event: {@Class} at {Dt}", GetType().Name, DateTime.UtcNow.ToLongTimeString());

        using (_channel = _connection.CreateModel())
        {
            _channel.ExchangeDeclare(
                exchange: _exchangeName,
                type: ExchangeType.Direct);

            var dto = JsonSerializer.SerializeToUtf8Bytes(eventDto);

            _channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: string.Empty,
                basicProperties: null,
                body: dto);
        }
    }

    public void Dispose()
    {
        if (!_channel.IsClosed && _channel != null)
        {
            _channel.Close();
        }
    }
}