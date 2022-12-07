using CoreServicesTemplate.Console.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;

namespace CoreServicesTemplate.Console.Common.Interfaces.IFeatures
{
    public interface IReadUsersFeature : IQueryHandler<UsersModel> { }
}