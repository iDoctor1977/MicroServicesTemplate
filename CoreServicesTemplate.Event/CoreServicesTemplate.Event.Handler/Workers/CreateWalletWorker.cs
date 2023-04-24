using System.Text;
using System.Text.Json;
using CoreServicesTemplate.Shared.Core.DtoEvents;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace CoreServicesTemplate.Event.Handler.Workers
{
    public class CreateWalletWorker : BackgroundService
    {
        private readonly ILogger<CreateWalletWorker> _logger;

        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private const string QUEUE_NAME = "createwallet.queue";

        public CreateWalletWorker(ILogger<CreateWalletWorker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Queue [{QUEUE_NAME}] is waiting for messages.");

            _connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };

            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclarePassive(QUEUE_NAME);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                stoppingToken.ThrowIfCancellationRequested();

                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.Received += async (bc, ea) =>
                {
                    var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                    _logger.LogInformation($"Processing msg: '{body}'.");
                    try
                    {
                        var eventDto = JsonSerializer.Deserialize<CreateWalletEventDto>(body);
                        _logger.LogInformation($"Sending order #{eventDto.IsCreated} confirmation email to [{eventDto.OwnerGuid}].");

                        await Task.Delay(new Random().Next(1, 3) * 1000, stoppingToken); // simulate an async email process

                        _logger.LogInformation($"Order #{eventDto.OwnerGuid} confirmation email sent.");
                        _channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (JsonException)
                    {
                        _logger.LogError($"JSON Parse Error: '{body}'.");
                        _channel.BasicNack(ea.DeliveryTag, false, false);
                    }
                    catch (AlreadyClosedException)
                    {
                        _logger.LogInformation("RabbitMQ is closed!");
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(default, e, e.Message);
                    }
                };

                _channel.BasicConsume(queue: QUEUE_NAME, autoAck: false, consumer: consumer);

                await Task.Delay(1000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("RabbitMQ connection is closed.");

            await base.StopAsync(cancellationToken);

            _connection.Close();
        }
    }
}