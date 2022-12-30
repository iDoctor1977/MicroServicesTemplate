using System.Globalization;
using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Dashboard.Web.Models;
using CoreServicesTemplate.Dashboard.Web.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Interfaces.IConsolidators;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CoreServicesTemplate.Dashboard.Web.Testing.HomeController
{
    [Collection("BaseTest")]
    public class AddUserAsync
    {
        private readonly TestFixtureBase _fixture;

        public AddUserAsync(TestFixtureBase fixture)
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

            _fixture.StorageRoomServiceMock.Setup(service => service.AddUserAsync(It.IsAny<UserApiModel>()));

            var controller = new Controllers.HomeController(
                _fixture.ServiceProvider.GetRequiredService<IConsolidator<UserViewModel, UserModel>>(),
                _fixture.ServiceProvider.GetRequiredService<IConsolidator<UsersViewModel, UsersModel>>(),
                _fixture.ServiceProvider.GetRequiredService<IFeatureCommand<UserModel>>(),
                _fixture.ServiceProvider.GetRequiredService<IFeatureQuery<UsersModel>>(),
                _fixture.LoggerMock.Object);

            //Act
            await controller.Add(userViewModel);

            //Assert
            _fixture.StorageRoomServiceMock.Verify((c => c.AddUserAsync(It.Is<UserApiModel>(arg => arg.Name == userViewModel.Name))));
        }
    }
}