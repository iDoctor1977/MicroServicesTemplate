using System.Collections.Generic;
using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Common.Interfaces.IFeatures
{
    public interface IReadUsersFeature : ICqrsQueryAsync<ICqrsQueryBase, IEnumerable<UserApiModel>> { }
}