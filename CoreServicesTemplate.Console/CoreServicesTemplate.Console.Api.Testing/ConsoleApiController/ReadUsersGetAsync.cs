using System;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Api.Testing.Fixtures;
using CoreServicesTemplate.Console.Common.Interfaces.IFeatures;
using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Shared.Core.Builders;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace CoreServicesTemplate.Console.Api.Testing.ConsoleApiController
{
    [Collection("BaseTest")]
    public class ReadUsersGetAsync
    {
        private readonly TestFixtureBase _fixture;

        public ReadUsersGetAsync(TestFixtureBase fixture)
        {
            _fixture = fixture;
            _fixture.GenerateHost();
        }

        [Fact]
        public async Task Should_Execute_Read_Users()
        {
            //Arrange
            var builder = new UserModelBuilder();
            var usersList = builder
                .AddUser("Foo", "Foo Foo", DateTime.Now.AddDays(-12569))
                .AddUser("Duffy", "Duck", DateTime.Now.AddDays(-11398))
                .AddUser("Micky", "Mouse", DateTime.Now.AddDays(-168963))
                .Build();

            var model = new UsersApiModel
            {
                UsersApiModelList = usersList
            };

            _fixture.StorageRoomServiceMock.Setup(service => service.ReadUsersAsync()).ReturnsAsync(model);

            var controller = new Controllers.ConsoleApiController(
                _fixture.ServiceProvider.GetRequiredService<IReadUsersFeature>(),
                _fixture.ServiceProvider.GetRequiredService<IConsolidators<UsersModel, UsersApiModel>>(),
                _fixture.LoggerMock.Object);

            //Act
            var result = await controller.Get();

            //Assert
            _fixture.StorageRoomServiceMock.Verify((method => method.ReadUsersAsync()), Times.Once());
            result.UsersApiModelList.Should().AllBeOfType<UserApiModel>().And.HaveCountGreaterThan(0);
        }
    }
}
