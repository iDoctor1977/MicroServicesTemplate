using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IDomainEntities;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;

public interface IUserRoot : IAggregateRoot
{
    public Task<UserAggModel> CreateUser(UserAggModel userValueObject);
    public Task<string> UserToString();
    public Task<string> AddressToString();
}