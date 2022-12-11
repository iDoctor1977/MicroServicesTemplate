using CoreServicesTemplate.Dashboard.Common.Models;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;

namespace CoreServicesTemplate.Dashboard.Common.Interfaces.IFeatures
{
    public interface IAddUserFeature : IQueryHandler<UserModel, HttpResponseMessage> { }
}