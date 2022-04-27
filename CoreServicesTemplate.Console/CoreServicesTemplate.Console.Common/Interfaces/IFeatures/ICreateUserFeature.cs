using System.Net.Http;
using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.Console.Common.Interfaces.IFeatures
{
    public interface ICreateUserFeature : ICqrsQueryAsync<UserApiModel, HttpResponseMessage> { }
}