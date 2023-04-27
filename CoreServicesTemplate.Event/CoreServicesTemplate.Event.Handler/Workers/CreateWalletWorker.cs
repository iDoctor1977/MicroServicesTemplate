using System.Text;
using System.Text.Json;
using CoreServicesTemplate.Shared.Core.DtoEvents;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace CoreServicesTemplate.Event.Handler.Workers
{
    public class CreateWalletWorker : WorkerBase<CreateWalletWorker>
    {
        public CreateWalletWorker(ConnectionFactory connectionFactory, string queueName, ILogger<CreateWalletWorker> logger) : base(connectionFactory, queueName, logger) { }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                stoppingToken.ThrowIfCancellationRequested();

                var consumer = new AsyncEventingBasicConsumer(Channel);
                consumer.Received += async (bc, ea) =>
                {
                    var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                    Logger.LogInformation($"Processing msg: '{body}'.");
                    try
                    {
                        var eventDto = JsonSerializer.Deserialize<CreateWalletEventDto>(body);
                        Logger.LogInformation($"Sending order #{eventDto.IsCreated} confirmation email to [{eventDto.OwnerGuid}].");

                        await Task.Delay(new Random().Next(1, 3) * 1000, stoppingToken); // simulate an async email process

                        Logger.LogInformation($"Order #{eventDto.OwnerGuid} confirmation email sent.");
                        Channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (JsonException)
                    {
                        Logger.LogError($"JSON Parse Error: '{body}'.");
                        Channel.BasicNack(ea.DeliveryTag, false, false);
                    }
                    catch (AlreadyClosedException)
                    {
                        Logger.LogInformation("RabbitMQ is closed!");
                    }
                    catch (Exception e)
                    {
                        Logger.LogError(default, e, e.Message);
                    }
                };

                Channel.BasicConsume(queue: QueueName, autoAck: false, consumer: consumer);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}