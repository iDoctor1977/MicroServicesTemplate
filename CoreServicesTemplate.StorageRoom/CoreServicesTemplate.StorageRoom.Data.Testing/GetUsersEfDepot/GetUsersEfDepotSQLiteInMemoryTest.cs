using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Api;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;
using CoreServicesTemplate.StorageRoom.Common.Models.AppModels;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using CoreServicesTemplate.StorageRoom.Data.Testing.Fixtures;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CoreServicesTemplate.StorageRoom.Data.Testing.GetUsersEfDepot
{
    public class GetUsersEfDepotSQLiteInMemoryTest : IClassFixture<DataCustomWebApplicationFactory<Program>>
    {
        private readonly DataCustomWebApplicationFactory<Program> _factory;

        public GetUsersEfDepotSQLiteInMemoryTest(DataCustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Should_Get_All_Users_From_InMemorySQLiteDb()
        {
            // Arrange
            var depot = new ORMFrameworks.EntityFramework.Depots.GetUsersEfDepot(_factory.Services.GetRequiredService<IDbContextWrap>(),
                _factory.Services.GetRequiredService<ICustomMapper<UsersAppModel, IEnumerable<User>>>(),
                _factory.Services.GetRequiredService<IUserRepository>());

            // Act
            var response = depot.ExecuteAsync();

            // Assert
            response.Result.Value.Should().BeOfType<UsersAppModel>();
            response.Result.Value?.UsersModelList.Should().BeOfType(typeof(List<UserAppModel>)).And.HaveCount(2);
        }
    }
}