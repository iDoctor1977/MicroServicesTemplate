using CoreServicesTemplate.Bus.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Bus.Common.Interfaces.IServices;
using CoreServicesTemplate.Bus.Core.Features;
using CoreServicesTemplate.Bus.Handler.Workers;
using CoreServicesTemplate.Bus.Services;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddTransient<ISendEmailFeature, SendEmailFeature>();
        services.AddTransient<IBusService, BusService>();

        services.AddHostedService(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var feature = sp.GetRequiredService<ISendEmailFeature>();
            var logger = sp.GetRequiredService<ILogger<WalletCreatedWorker>>();

            var factory = new ConnectionFactory { HostName = configuration["BusConnectionName"], DispatchConsumersAsync = true };
            var exchangeName = configuration["CreateWalletExchangeName"];
            var queueName = configuration["CreateWalletQueueName"];

            return new WalletCreatedWorker(factory, feature, exchangeName, queueName, logger);
        });
    })
    .Build();

host.Run();
