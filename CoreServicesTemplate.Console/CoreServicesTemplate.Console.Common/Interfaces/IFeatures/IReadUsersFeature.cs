using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Common.Interfaces.IFeatures
{
    public interface IReadUsersFeature : ICqrsQueryHandler<UsersApiModel> { }
}