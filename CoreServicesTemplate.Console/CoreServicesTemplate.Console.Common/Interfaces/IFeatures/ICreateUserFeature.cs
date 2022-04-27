using System.Net.Http;

namespace CoreServicesTemplate.Console.Common.Interfaces.IFeatures
{
    public interface ICreateUserFeature : ICqrsQueryAsync<UserApiModel, HttpResponseMessage> { }
}