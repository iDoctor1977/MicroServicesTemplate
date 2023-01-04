﻿using CoreServicesTemplate.Shared.Core.Interfaces.Models;

namespace CoreServicesTemplate.StorageRoom.Common.Models;

public class AddressAppModel : IAppModel
{
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
}