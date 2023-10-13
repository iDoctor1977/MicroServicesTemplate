using System.Text;
using System.Text.Json;
using CoreServicesTemplate.Bus.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Shared.Core.EventModels.Wallet;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace CoreServicesTemplate.Bus.Handler.Workers
{
    public class WalletCreatedWorker : WorkerBase
    {
        private readonly ISendEmailFeature _sendEmailEventFeature;

        public WalletCreatedWorker(
            IConnectionFactory connectionFactory,
            ISendEmailFeature sendEmailEventFeature,
            string exchangeName,
            string queueName,
            ILogger<WalletCreatedWorker> logger) : base(connectionFactory, exchangeName, queueName, logger)
        {
            _sendEmailEventFeature = sendEmailEventFeature;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = SetAsyncEventConsumer(stoppingToken);
            consumer.Received += async (model, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                Logger.LogInformation($"Processing msg: '{body}'.");

                try
                {
                    var bustDto = JsonSerializer.Deserialize<WalletCreatedBusDto>(body);

                    await _sendEmailEventFeature.ExecuteAsync(bustDto);

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