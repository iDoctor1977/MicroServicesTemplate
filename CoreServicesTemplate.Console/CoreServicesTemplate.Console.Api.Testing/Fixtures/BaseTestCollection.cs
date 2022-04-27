using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CoreServicesTemplate.Console.Api.Testing.Fixtures
{
    [CollectionDefinition("BaseTest")]
    public class BaseTestCollection : ICollectionFixture<BaseTestFixture>, IClassFixture<WebApplicationFactory<Startup>> { }
}
