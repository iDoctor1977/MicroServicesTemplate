using System.Collections.Generic;

namespace CoreServicesTemplate.Console.Common.Interfaces.IFeatures
{
    public interface IReadUsersFeature : ICqrsQueryAsync<object, IEnumerable<UserApiModel>> { }
}