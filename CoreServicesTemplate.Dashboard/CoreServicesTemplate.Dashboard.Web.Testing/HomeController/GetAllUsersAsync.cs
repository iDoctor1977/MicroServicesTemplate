using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Dashboard.Web.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CoreServicesTemplate.Dashboard.Web.Testing.HomeController
{
    [Collection("BaseTest")]
    public class GetAllUsersAsync
    {
        private readonly TestFixtureBase _fixture;

        public GetAllUsersAsync(TestFixtureBase fixture)
        {
            _fixture = fixture;
            _fixture.GenerateHost();
        }

        [Fact]
        public async Task Should_Execute_Reading_Users_With_StorageRoomServiceMock()
        {
            //Arrange
            var builder = new UserApiModelBuilder();
            var users = builder
                .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-12369))
                .AddUser("Matt", "Daemon", DateTime.Now.AddDays(-36982))
                .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-11023))
                .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-693983))
                .Build();

            var model = new UsersApiModel()
            {
                UsersApiModelList = users
            };

            _fixture.StorageRoomServiceMock.Setup(service => service.GetUsersAsync()).ReturnsAsync(model);

            var controller = new Controllers.HomeController(
                _fixture.ServiceProvider.GetRequiredService<IConsolidators<UserViewModel, UserModel>>(),
                _fixture.ServiceProvider.GetRequiredService<IConsolidators<UsersModel, UsersViewModel>>(),
                _fixture.ServiceProvider.GetRequiredService<IAddUserFeature>(),
                _fixture.ServiceProvider.GetRequiredService<IGetUsersFeature>(),
                _fixture.LoggerMock.Object);

            //Act
            var result = await controller.GetAll();

            //Assert
            _fixture.StorageRoomServiceMock.Verify((c => c.GetUsersAsync()), Times.Once);
        }
    }
}