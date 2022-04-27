using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures
{
    public interface ICreateUserFeature : ICqrsCommandAsync<UserApiModel> { }
}