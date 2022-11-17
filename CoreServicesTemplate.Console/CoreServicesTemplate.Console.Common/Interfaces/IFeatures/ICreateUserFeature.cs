using System.Net.Http;
using CoreServicesTemplate.Shared.Core.Interfaces.IFeatureHandles;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Common.Interfaces.IFeatures
{
    public interface ICreateUserFeature : IQueryHandler<UserApiModel, HttpResponseMessage> { }
}