using CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.Interfaces;

public interface IUserAggregate : IAggregate<UserAggModel>
{
    string UserToString();
    string AddressToString();
}