using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.StorageRoom.Common.Models.AggModels.Address;

public class AddressAggModelBase : IAggModel
{
    public Guid GuId { get; set; }
    public string Address1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
}