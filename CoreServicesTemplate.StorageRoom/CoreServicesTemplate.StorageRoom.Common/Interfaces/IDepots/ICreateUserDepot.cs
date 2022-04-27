using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IDepots
{
    public interface ICreateUserDepot : ICqrsCommandAsync<UserApiModel> { }
}