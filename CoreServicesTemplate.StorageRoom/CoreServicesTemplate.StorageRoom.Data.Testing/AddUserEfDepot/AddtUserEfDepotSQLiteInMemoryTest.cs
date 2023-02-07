using CoreServicesTemplate.Shared.Core.Interfaces.IMappers;
using CoreServicesTemplate.StorageRoom.Common.Models;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using CoreServicesTemplate.StorageRoom.Data.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using CoreServicesTemplate.StorageRoom.Data.Testing.Fixtures;
using CoreServicesTemplate.Shared.Core.Enums;
using CoreServicesTemplate.StorageRoom.Api;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDbContexts;

namespace CoreServicesTemplate.StorageRoom.Data.Testing.AddUserEfDepot
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
            var depot = new ORMFrameworks.EntityFramework.Depots.AddUserEfDepot(_factory.Services.GetRequiredService<IDbContextWrap>(),
                _factory.Services.GetRequiredService<IDefaultMapper<UserAppModel, User>>(),
                _factory.Services.GetRequiredService<IUserRepository>());

            // Act
            var user = new UserAppModel
            {
                Name = "Walter",
                Surname = "Mazarin",
                Birth = DateTime.Today.Add(new TimeSpan(-1323068)),
                AddressAppModel = new AddressAppModel
                {
                    Address1 = "Via Scandinavia, 20",
                    Address2 = "Boars {FE)",
                    City = "Ferrari",
                    PostalCode = "44100",
                    State = "Italy"
                }
            };

            var response = depot.ExecuteAsync(user);

            // Assert
            response.Result.Should().BeOfType<OperationStatusResult>().And.Be(OperationStatusResult.Created);
        }
    }
}