using System.Threading.Tasks;
using CoreServicesTemplate.Shared.Core.Interfaces.IDomainEntities;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;

public interface IAddressItem : IAggregateEntity
{
    Task<string> AddressToString();
}