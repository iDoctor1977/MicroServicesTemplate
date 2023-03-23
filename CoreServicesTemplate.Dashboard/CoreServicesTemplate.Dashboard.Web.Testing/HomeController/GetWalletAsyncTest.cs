using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Web.Models.Wallets;
using CoreServicesTemplate.Dashboard.Web.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace CoreServicesTemplate.Dashboard.Web.Testing.HomeController
{
    public class GetWalletAsyncTest : IClassFixture<WebCustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebCustomWebApplicationFactory<Program> _factory;

        public GetWalletAsyncTest(WebCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Execute_Reading_Users_With_StorageRoomServiceMock()
        {
            //Arrange
            var userBuilder = new UserApiModelBuilder();
            var users = userBuilder
                .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-12369))
                .AddUser("Matt", "Daemon", DateTime.Now.AddDays(-36982))
                .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-11023))
                .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-693983))
                .Build();

            var model = new UsersApiModel()
            {
                UsersApiModelList = users
            };

            _factory.StorageRoomServiceMock.Setup(service => service.GetWalletAsync()).ReturnsAsync(model);

            var controller = new Controllers.HomeController(
                _factory.Services.GetRequiredService<ICustomMapper<CreateWalletViewModel, UserAppModel>>(),
                _factory.Services.GetRequiredService<ICustomMapper<WalletItemsViewModel, UsersAppModel>>(),
                _factory.Services.GetRequiredService<ICreateWalletFeature>(),
                _factory.Services.GetRequiredService<IGetWalletFeature>(),
                _factory.Services.GetRequiredService<ILogger<Controllers.WalletController>>());

            //Act
            var result = await controller.GetAll();

            //Assert
            _factory.StorageRoomServiceMock.Verify((c => c.GetWalletAsync()), Times.Once);
        }
    }
}