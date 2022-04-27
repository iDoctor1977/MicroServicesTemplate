using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CoreServicesTemplate.StorageRoom.Api.Testing.Fixtures
{
    [CollectionDefinition("BaseTest")]
    public class BaseTestCollection : ICollectionFixture<BaseTestFixture>, IClassFixture<WebApplicationFactory<Startup>> { }
}
