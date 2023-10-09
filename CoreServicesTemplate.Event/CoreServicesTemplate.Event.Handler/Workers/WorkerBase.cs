using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoreServicesTemplate.Event.Handler.Workers;

public abstract class WorkerBase : BackgroundService
{
    private readonly IConnectionFactory _connectionFactory;
    private readonly string _exchangeName;

    private IConnection _connection;

    protected IModel Channel { get; private set; }
    protected string QueueName { get; }
    protected ILogger Logger { get; }

    public WorkerBase(
        IConnectionFactory connectionFactory,
        string exchangeName,
        string queueName,
        ILogger logger)
    {
        _connectionFactory = connectionFactory;
        _exchangeName = exchangeName;
        QueueName = queueName;

        Logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Queue [{QueueName}] is waiting for messages.");

        _connection = _connectionFactory.CreateConnection();
        Channel = _connection.CreateModel();

        return base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("RabbitMQ connection is closed.");

        await base.StopAsync(cancellationToken);
    }

    //Publish/Subscribe
    protected AsyncEventingBasicConsumer SetAsyncEventConsumer(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        Channel.ExchangeDeclare(
            exchange: _exchangeName,
            type: ExchangeType.Direct);

        Channel.QueueDeclare(queue: QueueName);

        Channel.QueueBind(
            queue: QueueName,
            exchange: _exchangeName,
            routingKey: string.Empty);

        return new AsyncEventingBasicConsumer(Channel);
    }
}