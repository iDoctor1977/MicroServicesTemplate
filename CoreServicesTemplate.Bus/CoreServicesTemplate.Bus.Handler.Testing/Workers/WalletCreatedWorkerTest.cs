using CoreServicesTemplate.Bus.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Bus.Handler.Testing.Fixtures;
using CoreServicesTemplate.Bus.Handler.Workers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CoreServicesTemplate.Bus.Handler.Testing.Workers
{
    public class WalletCreatedWorkerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private readonly IConnectionFactory _connectionFactory;
        private readonly ISendEmailFeature _sendEmailFeature;
        private readonly ILogger<WalletCreatedWorker> _logger;

        public WalletCreatedWorkerTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _connectionFactory = _factory.Services.GetRequiredService<IConnectionFactory>();
            _sendEmailFeature = _factory.Services.GetRequiredService<ISendEmailFeature>();
            _logger = _factory.Services.GetRequiredService<ILogger<WalletCreatedWorker>>();
        }

        [Fact]
        public async Task StartAsyncThenDisposeTriggersCancelledToken()
        {
            // Arrange
            //var service = new WalletCreatedWorker(_connectionFactory, _sendEmailFeature, "e", "q", _logger);
            var service = _factory.Services.GetRequiredService<BackgroundService>();

            // Act
            await service.StartAsync(CancellationToken.None);
            service.Dispose();
        }

        [Fact]
        public void CreateAndDisposeShouldNotThrow()
        {
            // Arrange
            var service = new WalletCreatedWorker(_connectionFactory, _sendEmailFeature, "e", "q", _logger);

            // Act
            service.Dispose();
        }
    }
}