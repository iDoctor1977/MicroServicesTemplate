using CoreServicesTemplate.Shared.Core.Interfaces.IModels;
using CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Address;

namespace CoreServicesTemplate.StorageRoom.Common.Models.AggModels.User;

public class UserAggModelBase : IAggModel
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public AddressAggModel AddressAggModel { get; set; }
}