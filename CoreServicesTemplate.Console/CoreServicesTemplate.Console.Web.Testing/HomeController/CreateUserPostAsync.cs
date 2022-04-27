using System;
using System.Globalization;
using System.Threading.Tasks;
using CoreServicesTemplate.Console.Web.Models;
using CoreServicesTemplate.Console.Web.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Models;
using Moq;
using Xunit;

namespace CoreServicesTemplate.Console.Web.Testing.HomeController
{
    [Collection("BaseTest")]
    public class CreateUserPostAsync
    {
        private readonly BaseTestFixture _fixture;

        public CreateUserPostAsync(BaseTestFixture fixture)
        {
            _fixture = fixture;
            _fixture.GenerateHost();
        }

        [Fact]
        public async Task Should_Execute_Creation_New_User_With_StorageRoomServiceMock()
        {
            //Arrange
            var userViewModel = new UserViewModel
            {
                Name = "Foo",
                Surname = "Foo Foo",
                Birth = DateTime.Now.AddDays(-26985).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
            };

            _fixture.StorageRoomServiceMock.Setup(service => service.CreateUserAsync(It.IsAny<UserApiModel>()));

            var controller = new Controllers.HomeController(_fixture.ServiceProvider, _fixture.LoggerMock.Object);

            //Act
            await controller.Create(userViewModel);

            //Assert
            _fixture.StorageRoomServiceMock.Verify((c => c.CreateUserAsync(It.Is<UserApiModel>(arg => arg.Name == userViewModel.Name))));
        }
    }
}