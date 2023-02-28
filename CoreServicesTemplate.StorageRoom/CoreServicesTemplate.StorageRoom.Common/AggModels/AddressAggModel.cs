using CoreServicesTemplate.Shared.Core.Interfaces.IModels;

namespace CoreServicesTemplate.StorageRoom.Common.AggModels;

public class AddressAggModel : IAggModel
{
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
}