using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures.Bases
{
    [CollectionDefinition("RepositoryTestBase")]
    public class TestCollectionRepositoryBase : IClassFixture<TestFixtureRepositories>, IClassFixture<WebApplicationFactory<Startup>> { }
}
