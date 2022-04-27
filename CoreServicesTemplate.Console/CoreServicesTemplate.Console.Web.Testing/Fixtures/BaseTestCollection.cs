using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CoreServicesTemplate.Console.Web.Testing.Fixtures
{
    [CollectionDefinition("BaseTest")]
    public class BaseTestCollection : ICollectionFixture<BaseTestFixture>, IClassFixture<WebApplicationFactory<Startup>> { }
}
