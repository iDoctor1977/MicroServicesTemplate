using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures.Bases
{
    [CollectionDefinition("DepotTestBase")]
    public class TestCollectionDepotBase : IClassFixture<TestFixtureDepots>, IClassFixture<WebApplicationFactory<Startup>> { }
}
