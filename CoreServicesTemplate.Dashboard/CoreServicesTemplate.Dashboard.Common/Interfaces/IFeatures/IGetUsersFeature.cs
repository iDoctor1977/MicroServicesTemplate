using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandlers;

namespace CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures;

public interface IGetUsersFeature : IQueryHandlerFeature<UsersAppModel> { }