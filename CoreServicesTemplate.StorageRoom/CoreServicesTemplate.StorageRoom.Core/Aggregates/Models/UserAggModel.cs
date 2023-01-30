using CoreServicesTemplate.Shared.Core.Interfaces.IModels;
using CoreServicesTemplate.StorageRoom.Core.Aggregates.Bases;

namespace CoreServicesTemplate.StorageRoom.Core.Aggregates.Models;

public class UserAggModel : AggModelBase, IAggModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birth { get; set; }

    public AddressAggModel AddressAggModel { get; set; }
}