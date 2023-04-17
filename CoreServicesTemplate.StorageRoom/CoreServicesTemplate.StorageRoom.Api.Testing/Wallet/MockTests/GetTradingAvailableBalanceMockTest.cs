using System.Collections;
using System.Net;
using System.Net.Http.Json;
using CoreServicesTemplate.Shared.Core.Infrastructures;
using CoreServicesTemplate.Shared.Core.Results;
using CoreServicesTemplate.StorageRoom.Api.Testing.Builders;
using CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Wallet;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.WalletItem;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Wallet.MockTests
{
    public class GetTradingAvailableBalanceMockTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        private Mock<IGetTradingAvailableBalanceDepot> GetTradingAvailableBalanceDepotMock { get; set; }

        private static readonly string UrlGet = ApiUrl.StorageRoom.GetTradingAvailableBalance();

        public GetTradingAvailableBalanceMockTest(WebApplicationFactory<Program> factory)
        {
            GetTradingAvailableBalanceDepotMock = new Mock<IGetTradingAvailableBalanceDepot>();

            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.Replace(new ServiceDescriptor(typeof(IGetTradingAvailableBalanceDepot), GetTradingAvailableBalanceDepotMock.Object));
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Theory]
        [ClassData(typeof(WalletFactory))]
        public async Task Should_Be_Return_Trading_Available_Decimal_Value(WalletModel walletModel, HttpOperationResult<decimal> operationResult)
        {
            // Arrange
            GetTradingAvailableBalanceDepotMock
                .Setup(depot => depot.ExecuteAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new OperationResult<WalletModel>(walletModel));

            var uri = $"{UrlGet}/{walletModel.OwnerGuid}";

            // Act
            var response = await _client.GetAsync(uri);

            // Assert
            response.StatusCode.Should().Be(operationResult.StatusCode);
            (await response.Content.ReadFromJsonAsync<decimal>()).Should().Be(operationResult.Value);
        }
    }

    public class WalletFactory : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var ownerGuid = new List<Guid>
            {
                new ("2d44253a-9489-4355-81a6-aba6e3e587cc"),
                new ("a1d5be2d-a93a-4523-a0cd-d5a405f39531"),
                new ("d1e92246-f10e-4701-951c-309460e30e4c"),
            };

            var walletItemsValues = new Dictionary<Guid, List<decimal>>
            {
                { ownerGuid[0], new List<decimal> { 25m, 62m, 80m } },
                { ownerGuid[1], new List<decimal> { 125.09m, 620.12m, 721.25m } },
                { ownerGuid[2], new List<decimal> { 1.26m, 329.33m, 53.80m } }
            };

            var walletItems = new Dictionary<Guid, ICollection<WalletItemModel>>();

            IWalletItemModelBuilder walletItemModelBuilder = new WalletItemModelBuilder();
            foreach (var walletItemValue in walletItemsValues)
            {
                var walletItemModels = walletItemModelBuilder
                    .AddUpdateWalletItemModel("A2A", Guid.NewGuid(), walletItemValue.Value[0])
                    .AddUpdateWalletItemModel("Ferrari", Guid.NewGuid(), walletItemValue.Value[1])
                    .AddUpdateWalletItemModel("Hera", Guid.NewGuid(), walletItemValue.Value[2])
                    .Build();

                walletItems.Add(walletItemValue.Key, walletItemModels);
            }

            IWalletModelBuilder walletBuilder = new WalletModelBuilder();
            var walletModels = walletBuilder
                .AddUpdateWalletModel(ownerGuid[0], 400m, walletItems[ownerGuid[0]])
                .AddUpdateWalletModel(ownerGuid[1], 2000m, walletItems[ownerGuid[1]])
                .AddUpdateWalletModel(ownerGuid[2], 400m, walletItems[ownerGuid[2]])
                .Build();

            var operationResults = new List<HttpOperationResult<decimal>>
            {
                new()
                {
                    OwnerGuid = ownerGuid[0],
                    StatusCode = HttpStatusCode.OK,
                    Value = 233m
                },
                new()
                {
                    OwnerGuid = ownerGuid[1],
                    StatusCode = HttpStatusCode.OK,
                    Value = 533.54m
                },
                new()
                {
                    OwnerGuid = ownerGuid[2],
                    StatusCode = HttpStatusCode.OK,
                    Value = 15.61m
                }
            };

            return walletModels.Select(w => new object[] { w, operationResults.First(g => g.OwnerGuid == w.OwnerGuid) }).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class HttpOperationResult<T>
    {
        public Guid OwnerGuid { get; set; }
        public T Value { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}