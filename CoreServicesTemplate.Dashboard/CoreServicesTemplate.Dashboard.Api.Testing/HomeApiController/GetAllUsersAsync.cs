using CoreServicesTemplate.Dashboard.Api.Testing.Fixtures;
using CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CoreServicesTemplate.Dashboard.Api.Testing.HomeApiController
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
        public async Task Should_Execute_Read_Users()
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

            var controller = new Controllers.HomeApiController(
                _fixture.ServiceProvider.GetRequiredService<IGetUsersFeature>(),
                _fixture.ServiceProvider.GetRequiredService<IConsolidators<UsersModel, UsersApiModel>>(),
                _fixture.LoggerMock.Object);

            //Act
            var result = await controller.GetAll();

            //Assert
            _fixture.StorageRoomServiceMock.Verify((method => method.GetUsersAsync()), Times.Once());
            result.UsersApiModelList.Should().AllBeOfType<UserApiModel>().And.HaveCountGreaterThan(0);
        }
    }
}
