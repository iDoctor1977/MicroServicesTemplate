using CoreServicesTemplate.Bus.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Bus.Common.Interfaces.IServices;
using CoreServicesTemplate.Bus.Core.Features;
using CoreServicesTemplate.Bus.Handler.Workers;
using CoreServicesTemplate.Bus.Services;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<ISendEmailEventFeature, SendEmailEventFeature>();
        services.AddTransient<IEventService, EventService>();

        services.AddHostedService(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var feature = sp.GetRequiredService<ISendEmailEventFeature>();
            var logger = sp.GetRequiredService<ILogger<CreateWalletWorker>>();

            var factory = new ConnectionFactory { HostName = configuration["BusConnectionName"], DispatchConsumersAsync = true };
            var exchangeName = configuration["CreateWalletExchangeName"];
            var queueName = configuration["CreateWalletQueueName"];

            return new CreateWalletWorker(factory, feature, exchangeName, queueName, logger);
        });
    })
    .Build();

host.Run();
