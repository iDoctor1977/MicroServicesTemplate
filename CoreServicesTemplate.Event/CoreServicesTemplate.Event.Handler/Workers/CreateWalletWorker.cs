using System.Text;
using System.Text.Json;
using CoreServicesTemplate.Event.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Shared.Core.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace CoreServicesTemplate.Event.Handler.Workers
{
    public class CreateWalletWorker : WorkerBase
    {
        private readonly ISendEmailFeature _sendEmailFeature;

        public CreateWalletWorker(
            IConnectionFactory connectionFactory,
            ISendEmailFeature sendEmailFeature,
            string exchangeName,
            string queueName,
            ILogger<CreateWalletWorker> logger) : base(connectionFactory, exchangeName, queueName, logger)
        {
            _sendEmailFeature = sendEmailFeature;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = SetAsyncEventConsumer(stoppingToken);
            consumer.Received += async (ch, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                Logger.LogInformation($"Processing msg: '{body}'.");

                try
                {
                    var eventDto = JsonSerializer.Deserialize<CreateWalletEventDto>(body);

                    await _sendEmailFeature.ExecuteAsync(eventDto);

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

            Channel.BasicConsume(
                queue: QueueName,
                autoAck: false,
                consumer: consumer);

            return Task.CompletedTask;
        }
    }
}