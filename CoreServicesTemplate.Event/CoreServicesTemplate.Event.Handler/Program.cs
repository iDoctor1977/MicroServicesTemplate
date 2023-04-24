using CoreServicesTemplate.Event.Handler.Workers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<CreateWalletWorker>();
    })
    .Build();

host.Run();