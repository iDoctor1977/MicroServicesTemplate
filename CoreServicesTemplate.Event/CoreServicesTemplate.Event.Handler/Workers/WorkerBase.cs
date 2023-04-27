using RabbitMQ.Client;

namespace CoreServicesTemplate.Event.Handler.Workers;

public abstract class WorkerBase<T> : BackgroundService
{
    private readonly ConnectionFactory _connectionFactory;
    private IConnection _connection;

    protected IModel Channel { get; private set; }

    protected readonly string QueueName;
    protected readonly ILogger<T> Logger;

    public WorkerBase(ConnectionFactory connectionFactory, string queueName, ILogger<T> logger)
    {
        _connectionFactory = connectionFactory;
        QueueName = queueName;
        Logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Queue [{QueueName}] is waiting for messages.");

        _connection = _connectionFactory.CreateConnection();
        Channel = _connection.CreateModel();
        Channel.QueueDeclarePassive(QueueName);
        Channel.BasicQos(0, 1, false);

        return base.StartAsync(cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        Logger.LogInformation("RabbitMQ connection is closed.");

        await base.StopAsync(cancellationToken);

        Channel.Close();
        _connection.Close();
    }
}