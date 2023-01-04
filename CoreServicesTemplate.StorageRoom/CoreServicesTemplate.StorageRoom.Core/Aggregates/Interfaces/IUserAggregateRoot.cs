using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IDomainEntities;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;

public interface IUserAggregateRoot : IAggregateRoot
{
    public Task<UserAggModel> CreateUser(UserAggModel userAggModel);
    public Task<string> UserToString();
    public Task<string> AddressToString();
}