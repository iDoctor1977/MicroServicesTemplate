using CoreServicesTemplate.Dashboard.Common.Builders;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models.AppModels.Wallets;
using CoreServicesTemplate.Dashboard.Common.Models.DomainModels.Wallets;
using CoreServicesTemplate.Dashboard.Web.Models.Wallets;
using CoreServicesTemplate.Dashboard.Web.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.Dashboard.Web.Testing.HomeController
{
    public class GetWalletAsyncTest : IClassFixture<WebCustomWebApplicationFactory<Program>>
    {
        private readonly WebCustomWebApplicationFactory<Program> _factory;

        public GetWalletAsyncTest(WebCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_Execute_Reading_Wallet_From_StorageRoomServiceMock()
        {
            //Arrange
            var ownerGuid = Guid.NewGuid();

            IWalletModelAdded walletBuilder = new WalletModelBuilder();
            var wallets = walletBuilder.AddWallet(ownerGuid, 123589.63m, 236.9m, 124.2m).Build();

            _factory.GetWalletServiceMock.Setup(service => service.ExecuteAsync(ownerGuid)).ReturnsAsync(new OperationResult<WalletModel>(wallets.FirstOrDefault(x => x.OwnerGuid == ownerGuid) ?? throw new InvalidOperationException()));

            var controller = new Controllers.WalletController(
                _factory.Services.GetRequiredService<ICustomMapper<CreateWalletViewModel, CreateWalletAppModel>>(),
                _factory.Services.GetRequiredService<ICustomMapper<WalletViewModel, WalletAppModel>>(),
                _factory.Services.GetRequiredService<ICreateWalletFeature>(),
                _factory.Services.GetRequiredService<IGetWalletFeature>(),
                _factory.Services.GetRequiredService<ILogger<Controllers.WalletController>>());

            //Act
            var result = await controller.ReadWallet(ownerGuid);

            //Assert
            _factory.GetWalletServiceMock.Verify((c => c.ExecuteAsync(ownerGuid)), Times.Once);
        }
    }
}