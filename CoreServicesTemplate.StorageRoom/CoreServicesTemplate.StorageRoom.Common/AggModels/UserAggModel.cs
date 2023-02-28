using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.StorageRoom.Common.AggModels;

public class UserAggModel : IAggModel
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime Birth { get; set; }

    public AddressAggModel AddressAggModel { get; set; }
}