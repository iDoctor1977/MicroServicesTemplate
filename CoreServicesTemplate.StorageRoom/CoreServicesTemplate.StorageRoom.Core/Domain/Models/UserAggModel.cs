using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.StorageRoom.Core.Domain.Models;

public class UserAggModel : IAggModel
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birth { get; set; }

    public AddressAggModel AddressAggModel { get; set; }
}