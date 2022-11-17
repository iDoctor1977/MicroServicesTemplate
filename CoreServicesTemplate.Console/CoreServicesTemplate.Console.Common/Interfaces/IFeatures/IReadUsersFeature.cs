using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Common.Interfaces.IFeatures
{
    public interface IReadUsersFeature : IQueryHandler<UsersApiModel> { }
}