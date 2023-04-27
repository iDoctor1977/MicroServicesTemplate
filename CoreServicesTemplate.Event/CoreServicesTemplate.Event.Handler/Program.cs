using CoreServicesTemplate.Event.Handler.Workers;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var logger = sp.GetRequiredService<ILogger<CreateWalletWorker>>();

            var factory = new ConnectionFactory
            {
                HostName = configuration["BusConnectionName"],
                DispatchConsumersAsync = true
            };
            var queueName = configuration["SubscriptionClientName"];

            return new CreateWalletWorker(factory, queueName, logger);
        });
    })
    .Build();

host.Run();