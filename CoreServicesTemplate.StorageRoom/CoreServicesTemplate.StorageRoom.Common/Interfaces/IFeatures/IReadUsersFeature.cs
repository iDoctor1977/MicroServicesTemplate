using System.Collections.Generic;
using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.ICqrs;
using CoreServicesTemplate.Shared.Core.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Interfaces.IFeatures
{
    public interface IReadUsersFeature : ICqrsQueryAsync<int, IEnumerable<UserApiModel>>
    {
        Task<IEnumerable<UserApiModel>> ExecuteAsync();
    }
}