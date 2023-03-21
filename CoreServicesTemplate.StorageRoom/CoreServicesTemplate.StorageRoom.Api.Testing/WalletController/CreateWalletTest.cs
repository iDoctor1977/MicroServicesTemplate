using System.Net;
using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Models;
using CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures;
using CoreServicesTemplate.StorageRoom.Data.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.WalletController
{
    public class CreateWalletTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        private static readonly string UrlPost = ApiUrl.StorageRoom.Wallet.WalletUrlBase();

        public CreateWalletTest(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;

            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            _factory.OpenDbConnection();
        }

        public void Dispose()
        {
            _factory.CloseDbConnection();
        }

        [Fact]
        public async Task Should_Be_Create_New_Wallet_And_Save_It_To_InMemorySQLiteDb()
        {
            // Arrange
            SeedDatabaseForTest();

            var walletDto = new CreateWalletApiDto
            {
                OwnerGuid = Guid.NewGuid(),
                TradingAllowedBalance = 1.23m,
                OperationAllowedBalance = 12.3m,
                Balance = 2.36m
            };

            var uri = $"{UrlPost}/{walletDto}";

            // Act
            var response = await _client.PostAsJsonAsync(uri, walletDto);

            // Assert
            response.EnsureSuccessStatusCode();

            var dbContext = _factory.GetContext();
            dbContext?.Wallets.Should().HaveCount(2);
        }

        [Theory]
        [InlineData(null, "12.3", "32.6", "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", null, "32.6", "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "12.3", null, "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "12.3", "32.6", null, HttpStatusCode.UnprocessableEntity)]
        public async Task Should_Verify_Return_HttpStatusCode_To_Create_New_Wallet_With_Null_Values(
            string? ownerGuid, 
            string tradingAllowedBalance,
            string operationAllowedBalance,
            string balance, HttpStatusCode expectedResultCode)
        {
            // Arrange
            var og = Guid.Empty;
            if (ownerGuid != null)
            {
                og = Guid.Parse(ownerGuid);
            }
            var t = Convert.ToDecimal(tradingAllowedBalance);
            var o = Convert.ToDecimal(operationAllowedBalance);
            var b = Convert.ToDecimal(balance);

            var walletDto = new CreateWalletApiDto
            {
                OwnerGuid = og,
                TradingAllowedBalance = t,
                OperationAllowedBalance = o,
                Balance = b
            };

            var uri = $"{UrlPost}/{walletDto}";

            // Act
            var response = await _client.PostAsJsonAsync(uri, walletDto);

            // Assert
            var responseStatusCode = response.StatusCode;
            responseStatusCode.Should().Be(expectedResultCode);
        }

        [Theory]
        [InlineData(null, "12.3", "32.6", "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "-15.69", "32.6", "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "12.3", "-32.9", "12.5", HttpStatusCode.UnprocessableEntity)]
        [InlineData("8b4ff777-7bbc-496e-994e-ade927f37cfa", "12.3", "32.6", "-36.1", HttpStatusCode.UnprocessableEntity)]
        public async Task Should_Verify_Return_Exception_To_Create_New_Wallet_Negative_Values(
            string? ownerGuid, 
            string tradingAllowedBalance,
            string operationAllowedBalance,
            string balance, HttpStatusCode expectedResultCode)
        {
            // Arrange
            var og = Guid.Empty;
            if (ownerGuid != null)
            {
                og = Guid.Parse(ownerGuid);
            }
            var t = Convert.ToDecimal(tradingAllowedBalance);
            var o = Convert.ToDecimal(operationAllowedBalance);
            var b = Convert.ToDecimal(balance);

            var walletDto = new CreateWalletApiDto
            {
                OwnerGuid = og,
                TradingAllowedBalance = t,
                OperationAllowedBalance = o,
                Balance = b
            };

            var uri = $"{UrlPost}/ {walletDto}";

            // Act
            var response = await _client.PostAsJsonAsync(uri, walletDto);

            // Assert
            var responseStatusCode = response.StatusCode;
            responseStatusCode.Should().Be(expectedResultCode);
        }

        private void SeedDatabaseForTest()
        {
            var context = _factory.GetContext();

            if (context != null && !context.Database.EnsureCreatedAsync().Equals(null))
            {
                context.Wallets.Add(new Wallet
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

                context.SaveChanges();
            }
        }
    }
}