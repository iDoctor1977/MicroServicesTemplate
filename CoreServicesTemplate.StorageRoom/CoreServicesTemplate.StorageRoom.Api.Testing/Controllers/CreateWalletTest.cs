using System.Net;
using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models.Wallet;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Controllers
{
    public class CreateWalletTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        private static readonly string UrlPost = ApiUrl.StorageRoomApi.CreateWallet();

        public CreateWalletTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Should_Create_New_Wallet_And_Save_It_To_InMemorySQLiteDb()
        {
            // Arrange
            _factory.OpenDbConnection();

            SeedDatabaseForTest();

            var walletDto = new RequestStorageRoomCreateWalletApiDto()
            {
                OwnerGuid = Guid.NewGuid(),
                TradingAllowedBalance = 1.23m,
                OperationAllowedBalance = 12.3m,
                Balance = 2.36m
            };

            // Act
            var response = await _client.PostAsJsonAsync(UrlPost, walletDto);

            // Assert
            response.EnsureSuccessStatusCode();
            _factory.GetContext().Wallets.Should().HaveCount(2);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", "12.3", "32.6", "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", null, "32.6", "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "12.3", null, "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "12.3", "32.6", null, HttpStatusCode.UnprocessableEntity)]
        public async Task Should_Verify_Return_HttpStatusCode_To_Create_New_Wallet_With_Null_Values(
            string ownerGuid,
            string tradingAllowedBalance,
            string operationAllowedBalance,
            string balance, HttpStatusCode expectedResultCode)
        {
            // Arrange
            _factory.OpenDbConnection();

            var t = Convert.ToDecimal(tradingAllowedBalance);
            var o = Convert.ToDecimal(operationAllowedBalance);
            var b = Convert.ToDecimal(balance);

            var walletDto = new RequestStorageRoomCreateWalletApiDto()
            {
                OwnerGuid = Guid.Parse(ownerGuid),
                TradingAllowedBalance = t,
                OperationAllowedBalance = o,
                Balance = b
            };

            // Act
            var response = await _client.PostAsJsonAsync(UrlPost, walletDto);

            // Assert
            var responseStatusCode = response.StatusCode;
            responseStatusCode.Should().Be(expectedResultCode);
        }

        [Theory]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "-15.69", "32.6", "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "12.3", "-32.9", "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "12.3", "32.6", "-36.1", HttpStatusCode.UnprocessableEntity)]
        public async Task Should_Verify_Return_Exception_To_Create_New_Wallet_Negative_Values(
            string ownerGuid,
            string tradingAllowedBalance,
            string operationAllowedBalance,
            string balance, HttpStatusCode expectedResultCode)
        {
            // Arrange
            _factory.OpenDbConnection();

            SeedDatabaseForTest();

            var t = Convert.ToDecimal(tradingAllowedBalance);
            var o = Convert.ToDecimal(operationAllowedBalance);
            var b = Convert.ToDecimal(balance);

            var walletDto = new RequestStorageRoomCreateWalletApiDto()
            {
                OwnerGuid = Guid.Parse(ownerGuid),
                TradingAllowedBalance = t,
                OperationAllowedBalance = o,
                Balance = b
            };

            // Act
            var response = await _client.PostAsJsonAsync(UrlPost, walletDto);

            // Assert
            var responseStatusCode = response.StatusCode;
            responseStatusCode.Should().Be(expectedResultCode);
        }

        public void Dispose()
        {
            _factory.CloseDbConnection();
        }

        private void SeedDatabaseForTest()
        {
            if (!_factory.GetContext().Database.EnsureCreatedAsync().Equals(null))
            {
                _factory.GetContext().Wallets.Add(new Data.Entities.Wallet
                {
                    Guid = Guid.NewGuid(),
                    OwnerGuid = Guid.NewGuid(),
                    TradingAllowedBalance = 12.34m,
                    OperationAllowedBalance = 12.34m,
                    Balance = 1234,
                    Performance = 1234,
                    State = EntityState.Added,
                    DateCreated = DateTime.Now,
                    LastModifiedDate = DateTime.Now
                });

                _factory.GetContext().SaveChanges();
            }
        }
    }
}