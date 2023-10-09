using System.Globalization;
using System.Net;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models.Wallets;
using CoreServicesTemplate.Dashboard.Web.Models.Wallets;
using CoreServicesTemplate.Dashboard.Web.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.Shared.Core.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.Dashboard.Web.Testing.HomeController
{
    public class CreateWalletAsyncTest : IClassFixture<WebCustomWebApplicationFactory<Program>>
    {
        private readonly WebCustomWebApplicationFactory<Program> _factory;

        public CreateWalletAsyncTest(WebCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_Execute_Creation_New_User_With_StorageRoomServiceMock()
        {
            //Arrange
            var userViewModel = new CreateWalletViewModel
            {
                DayTime = DateTime.Now.AddDays(-26985).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture),
                Balance = "126986.32",
                TradingAllowedBalance = "2598.69",
                OperationAllowedBalance = "128.96"
            };

            _factory.StorageRoomServiceMock.Setup(service => service.CreateNewWalletAsync(It.IsAny<CreateWalletApiDto>())).ReturnsAsync(new OperationResult<HttpResponseMessage>(new HttpResponseMessage { StatusCode = HttpStatusCode.OK }));

            var controller = new Controllers.WalletController(
                _factory.Services.GetRequiredService<ICustomMapper<CreateWalletViewModel, CreateWalletAppModel>>(),
                _factory.Services.GetRequiredService<ICustomMapper<WalletViewModel, WalletAppModel>>(),
                _factory.Services.GetRequiredService<ICreateWalletFeature>(),
                _factory.Services.GetRequiredService<IGetWalletFeature>(),
                _factory.Services.GetRequiredService<ILogger<Controllers.WalletController>>());

            //Act
            var responseMessage = await controller.Create(userViewModel);

            //Assert
            _factory.StorageRoomServiceMock.Verify(method => method.CreateNewWalletAsync(It.IsAny<CreateWalletApiDto>()), Times.Once);
        }
    }
}