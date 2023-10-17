using CoreServicesTemplate.Bus.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Bus.Common.Interfaces.IServices;
using CoreServicesTemplate.Bus.Common.Models;
using CoreServicesTemplate.Bus.Core.Features;
using CoreServicesTemplate.Bus.Handler.Workers;
using CoreServicesTemplate.Shared.Core.Bases;
using CoreServicesTemplate.Shared.Core.BusModels.Wallet;
using CoreServicesTemplate.Shared.Core.Interfaces.IEvents;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Moq;
using RabbitMQ.Client;
using IConnectionFactory = RabbitMQ.Client.IConnectionFactory;

namespace CoreServicesTemplate.Bus.Handler.Testing.Workers
{
    public class WalletCreatedWorkerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        private readonly string _connectionName;
        private readonly string _exchangeName;
        private readonly IConnectionFactory _connectionFactory;

        private Mock<IBusService> BusServiceMock { get; set; }
        private readonly IEventBus<WalletCreatedBusDto> _eventBus;

        private readonly ISendEmailFeature _sendEmailFeature;

        public WalletCreatedWorkerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _connectionName = "connectiontTest";
            _exchangeName = "exchangeTest";
           _connectionFactory = new ConnectionFactory { HostName = _connectionName, DispatchConsumersAsync = true };

            BusServiceMock = new Mock<IBusService>();

            _client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IEventBus<WalletCreatedBusDto>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }
                    services.Replace(new ServiceDescriptor(typeof(IEventBus<WalletCreatedBusDto>), new WalletCreatedBusMock(_connectionFactory, _exchangeName, _factory.Services.GetRequiredService<ILogger<WalletCreatedBusMock>>())));
                    
                    services.Replace(new ServiceDescriptor(typeof(IBusService), BusServiceMock.Object));
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _sendEmailFeature = _factory.Services.GetRequiredService<ISendEmailFeature>();
            _eventBus = _factory.Services.GetRequiredService<IEventBus<WalletCreatedBusDto>>();
        }

        [Fact]
        public async Task StartAsyncThenDisposeTriggersCancelledToken()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var model = new EmailPropertiesModel
            {
                OwnerGuid = guid,
                Name = "Name",
                Surname = "Surname",
                Address = "Address",
                Cap = "CAP",
                FromAddress = "from.bus@servicebus.com",
                ToAddress = "to.bus@servicebus.com",
            };

            var payload = new WalletCreatedBusDto
            {
                OwnerGuid = guid, IsCreated = true
            };
            
            _eventBus.Publish(payload);

            BusServiceMock.Setup(bus => bus.GetEmailPropertiesAsync(It.IsAny<Guid>())).ReturnsAsync(new OperationResult<EmailPropertiesModel>(model));

            var service = new WalletCreatedWorker(_connectionFactory, _sendEmailFeature, "e", "q", _factory.Services.GetRequiredService<ILogger<WalletCreatedWorker>>());

            // Act
            await service.StartAsync(CancellationToken.None);
            service.Dispose();
        }

        [Fact]
        public void CreateAndDisposeShouldNotThrow()
        {
            // Arrange
            var service = new WalletCreatedWorker(_connectionFactory, _sendEmailFeature, _exchangeName, _connectionName, _factory.Services.GetRequiredService<ILogger<WalletCreatedWorker>>());

            // Act
            service.Dispose();
        }
    }

    public class WalletCreatedBusMock : EventBusBase<WalletCreatedBusDto>
    {
        public WalletCreatedBusMock(IConnectionFactory connectionFactory, string exchangeName, ILogger logger) : base(connectionFactory, exchangeName, logger) { }
    }
}